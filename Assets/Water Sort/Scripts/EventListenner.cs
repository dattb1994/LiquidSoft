using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiquidSoft
{
    public delegate void OnPostEvent();
    public delegate void OnPostInt(int param);
    public delegate void OnPostFloat(float param);
    public delegate void OnPostBool(bool param);
    public class EventListenner : MonoBehaviour
    {
        public static EventListenner instance;

        public OnPostInt OnLevelDone;
        public OnPostInt OnLevelIsSpawn;
        public OnPostEvent OnLevelLoadStardDone;
        public OnPostEvent OnStartLoadLevel;
        public OnPostEvent OnTimeOutConnectInternet;
        public OnPostEvent OnLevelIsOver;

        private void Awake()
        {
            instance = this;
        }
    } 
}
