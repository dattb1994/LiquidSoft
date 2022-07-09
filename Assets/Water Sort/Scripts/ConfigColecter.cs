using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LiquidSoft
{
    public class ConfigColecter : MonoBehaviour
    {
        public static ConfigColecter instance;

        public WaterSoftConfig softConfig;
        public LevelConfig levelConfig;
        //public GPConfig otherConfig;

        private void Awake()
        {
            instance = this;
        }
    }
}