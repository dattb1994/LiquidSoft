using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LiquidSoft
{
    public class LevelController : MonoBehaviour
    {
        public static LevelController instance;

        public LevelSpawner levelSpawner;

        public int levelSet;

        public int levelNow;
        public int LevelNow { 
            get
            {

                if (PlayerPrefs.HasKey("LevelNow"))
                    return PlayerPrefs.GetInt("LevelNow");
                else
                    return 1;
                    
            }
            set { PlayerPrefs.SetInt("LevelNow", value); } }

        public int levelUnlock;
        public int LevelUnlock {
            get
            {

                if (PlayerPrefs.HasKey("LevelUnlock"))
                    return PlayerPrefs.GetInt("LevelUnlock");
                else
                    return 1;

            }
            set { PlayerPrefs.SetInt("LevelUnlock", value); }
        }

        private void Awake()
        {
            instance = this;
        }
        private void Update()
        {
            levelNow = LevelNow;
            levelUnlock = LevelUnlock;
        }

        private void Start()
        {
            //EventListenner.instance.OnLevelDone += OnNextLevel;

            //if(levelSet>0)
                //LevelNow = levelSet;

            OnSpawnLevel(LevelNow);
        }
        public void OnNextLevel()
        {
            if (LevelNow > LevelUnlock)
                LevelUnlock += 1;

            LevelNow++;
            OnSpawnLevel(LevelNow);
        }
        private void OnNextLevel(int param)
        {
            if (param > LevelUnlock)
                LevelUnlock += 1;

            LevelNow++;
            OnSpawnLevel(LevelNow);
        }

        public void OnUnlockNewLevel(int newLevel)
        {
            LevelUnlock = newLevel;
        }
        public void OnUnlockNewLevel()
        {
            LevelUnlock += 1;
        }

        public void OnSpawnLevel(int levelPlay)
        {
            DisplayController.instance.ChangeTextLevel(levelPlay);
            LevelNow = levelPlay;
            levelSpawner.OnSpawnLevel(ConfigColecter.instance.levelConfig.levels[levelPlay-1]);

            EventListenner.instance.OnLevelIsSpawn?.Invoke(levelPlay);

        }
        public void DestroyLevel()
        {
            levelSpawner.OnDestroyLevel();
        }
    }
    [Serializable]
    public class LevelInfo
    {
        public string name = "Level ";
        public List<BottleInfo> bottles;
    }
    [Serializable]
    public class BottleInfo
    {
        public List<LiquidLevel> liquids;

    }

}
