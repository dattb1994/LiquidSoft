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
            //StartCoroutine(Check());
        }
        public string PathLink
        {
            get
            {
                string folderName = Application.dataPath + "/StreamingAssets";
                //DebugLog.instance.Add("init path link ");
#if UNITY_ANDROID && !UNITY_EDITOR
                    folderName = "jar:file://" + Application.dataPath + "!/assets";
                //DebugLog.instance.Add("init path link on android");
#endif
#if UNITY_IOS && !UNITY_EDITOR
                    folderName = Application.dataPath + "/Raw";
#endif

                //DebugLog.instance.Add("Path: "+ Path.Combine(folderName, nameFile));
                return Path.Combine(folderName, nameFile);
            }
        }
        public IEnumerator Check()
        {
            EventListenner.instance.OnStartLoadLevel?.Invoke();

            yield return WaitGetLevelLocal();
            //yield return WaitGetLevelApi();

            //if (levelConfig.levels.Count == 0)
            //{
                //UpdateLevelConfig();
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
            DebugLog.instance.Add("ConfigColecter.instance.levelConfig.levels.cout = " + ConfigColecter.instance.levelConfig.levels.Count);

            foreach (var item in levelConfig.levels)
            {
                ConfigColecter.instance.levelConfig.levels.Add(item);
            }
            DebugLog.instance.Add("Init level done! All ready");

            EventListenner.instance.OnLevelLoadStardDone?.Invoke();
        }
        IEnumerator WaitGetLevelLocal()
        {
            DebugLog.instance.Add("Start load local");
            UnityWebRequest www = UnityWebRequest.Get(PathLink);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                DebugLog.instance.Add("Get fail local with path : "+ PathLink);
            }
            else
            {
                DebugLog.instance.Add("Get success local with path : " + PathLink);
                string str = www.downloadHandler.text;
                print(str);
                if (str != "")
                {
                    levelConfig = JsonUtility.FromJson<LevelConfogToString>(str);
                }
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

            DebugLog.instance.Add("internet availbe!!!!");
            if (t <= 5)
            {
                print("Connect to internet!");
                DebugLog.instance.Add("Connect to internet!");
                App42iManager.GetData(this);
                while (levelFromApi.levels.Count == 0)
                    yield return null;
            }
            else
            {
                DebugLog.instance.Add("can't conect to internet!");
                print("can't conect to internet!");
                levelFromApi = new LevelConfogToString();
                EventListenner.instance.OnTimeOutConnectInternet?.Invoke();
            }


        }
        void UpdateLevelConfig()
        {
            DebugLog.instance.Add("Get data success with UpdateLevelConfig");
            levelConfig = new LevelConfogToString();
            foreach (var lv in levelFromApi.levels)
            {
                levelConfig.levels.Add(lv);
            }
            File.WriteAllText(PathLink, JsonUtility.ToJson(levelConfig));
            DebugLog.instance.Add("write all file done");

        }
        public void OnSuccess(object response)
        {
            DebugLog.instance.Add("Get data success with api");
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
            DebugLog.instance.Add("Get data fail");
        }
    }
}