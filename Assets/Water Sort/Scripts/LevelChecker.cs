using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
namespace LiquidSoft
{
    public class LevelChecker : MonoBehaviour, App42CallBack
    {
        public string nameFile = "levelConfig.json";

        public LevelConfogToString levelConfig = new LevelConfogToString();
        public LevelConfogToString levelFromApi = new LevelConfogToString();
        void Start()
        {
            StartCoroutine(Check());
        }
        public string PathLink
        {
            get
            {
                string folderName = Application.dataPath + "/StreamingAssets";
                if (Application.platform == RuntimePlatform.Android)
                    folderName = "jar:file://" + Application.dataPath + "!/assets";
                if (Application.platform == RuntimePlatform.IPhonePlayer)
                    folderName = Application.dataPath + "/Raw";

                return Path.Combine(folderName, nameFile);
            }
        }
        public IEnumerator Check()
        {
            EventListenner.instance.OnStartLoadLevel?.Invoke();

            yield return WaitGetLevelLocal();
            yield return WaitGetLevelApi();

            //if (levelConfig.levels.Count == 0)
            //{
                UpdateLevelConfig();
            //}
            //else
            //{
                //if (levelConfig.levels.Count < levelFromApi.levels.Count)
                //{
                //    UpdateLevelConfig();
                //}
                //else
                //{
                //    print("New level have not update!");
                //}
            //}
            yield return new WaitForSeconds(.1f);
            ConfigColecter.instance.levelConfig.levels.Clear();

            foreach (var item in levelConfig.levels)
            {
                ConfigColecter.instance.levelConfig.levels.Add(item);
            }

            EventListenner.instance.OnLevelLoadStardDone?.Invoke();
        }
        IEnumerator WaitGetLevelLocal()
        {
            UnityWebRequest www = UnityWebRequest.Get(PathLink);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string str = www.downloadHandler.text;
                print(str);
                if (str != "")
                    levelConfig = JsonUtility.FromJson<LevelConfogToString>(str);
                else
                    levelConfig = new LevelConfogToString();
            }
        }
        IEnumerator WaitGetLevelApi()
        {
            //Ping p = new Ping("https://www.google.com/");
            float t = 0;
            while (Application.internetReachability == NetworkReachability.NotReachable)
            {
                t += Time.deltaTime;
                if (t > 5) break;
                yield return null;
            }

            if (t <= 5)
            {
                print("Connect to internet!");
                App42iManager.GetData(this);
                while (levelFromApi.levels.Count == 0)
                    yield return null;
            }
            else
            {
                print("can't conect to internet!");
                levelFromApi = new LevelConfogToString();
                EventListenner.instance.OnTimeOutConnectInternet?.Invoke();
            }


        }
        void UpdateLevelConfig()
        {
            print("Just updated new level!");
            levelConfig = new LevelConfogToString();
            foreach (var lv in levelFromApi.levels)
            {
                levelConfig.levels.Add(lv);
            }
            File.WriteAllText(PathLink, JsonUtility.ToJson(levelConfig));

        }
        public void OnSuccess(object response)
        {
            print("Get data success");
            Storage storage = (Storage)response;
            IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
            string jsonString = "";
            for (int i = 0; i < jsonDocList.Count; i++)
            {
                //print(jsonDocList[i].GetJsonDoc());
                jsonString = jsonDocList[i].GetJsonDoc();
            }
            levelFromApi = new LevelConfogToString();

            levelFromApi = JsonUtility.FromJson<LevelConfogToString>(jsonString);
        }
        public void OnException(Exception ex)
        {
            print("Get data fail");
        }
    }
}