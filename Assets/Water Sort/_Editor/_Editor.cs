using com.shephertz.app42.paas.sdk.csharp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiquidSoft
{
    [ExecuteInEditMode]
    public class _Editor : MonoBehaviour, App42CallBack
    {
        public LevelInfo levelInfo;

        public LevelConfig levelConfig;

        public bool action = false;
        public bool resetLevel = false;
        public bool printColor = false;
        public bool printLevel;
        public bool pushToApi;

        public LevelConfig levelCOnfigMain;

        public Color color;
        public string StrColor;
        public string levelJson;
        public int levelSet;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (action)
            {
                OnAction();
            }

            if (resetLevel)
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.SetInt("LevelNow", levelSet);
                PlayerPrefs.SetInt("LevelUnlock", levelSet);


                resetLevel = false;
            }

            if (printColor)
            {
                StrColor = color.r + "f," + color.g + "f," + color.b + "f,1";
                printColor = false;
            }

            if (printLevel)
            {
                levelJson = "";
                levelJson = levelCOnfigMain.ToString();
                printLevel = false;
            }

            if (pushToApi)
            {
                string _levelJson = "";
                _levelJson = levelCOnfigMain.ToString();

                App42iManager.UpdateData(this,_levelJson);
                pushToApi = false;
                DisplayController.instance.OnButtonReplayClick();
            }
        }

        void OnAction()
        {
            for (int i = 0; i < 15; i++)
            {
                //còni
                levelCOnfigMain.levels.RemoveAt(0);
            }


            action = false;
        }

        public void OnSuccess(object response)
        {
            print("push succes");
        }

        public void OnException(Exception ex)
        {
            print("push fail");
        }
    }
}