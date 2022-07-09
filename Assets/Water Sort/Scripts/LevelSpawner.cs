using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiquidSoft
{
    public class LevelSpawner : MonoBehaviour
    {
        public List<GameObject> lv_bottles;

        private void Start()
        {
            EventListenner.instance.OnLevelDone += OnDestroyLevel;
        }
        public void OnSpawnLevel(LevelInfo info)
        {
            if (transform.childCount > 0)
                Destroy(transform.GetChild(0).gameObject);

            GameObject lv = new GameObject();
            Destroy(lv.gameObject);

            lv = Instantiate(lv_bottles[info.bottles.Count], transform);
            lv.transform.localPosition = Vector3.zero;
            GPController.instance.SetLevelPlaying(lv.transform);
            SetLiquidToBottle(lv, info);

        }
        public void OnDestroyLevel()
        {
            if (transform.childCount > 0)
                Destroy(transform.GetChild(0).gameObject);
        }
        public void OnDestroyLevel(int _lv)
        {
            if (transform.childCount > 0)
                Destroy(transform.GetChild(0).gameObject);
        }
        void SetLiquidToBottle(GameObject _lv, LevelInfo levelInfo)
        {
            for (int i = 0; i < levelInfo.bottles.Count; i++)
            {
                for (int j = 0; j < levelInfo.bottles[i].liquids.Count; j++)
                {
                    _lv.transform.GetChild(i).GetChild(0).GetComponent<BottleController>().levelsInit.Add(levelInfo.bottles[i].liquids[j]);
                }
            }
        }
    }
    
}
