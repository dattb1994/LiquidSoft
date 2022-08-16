using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StackSix
{
    public class LevelSpawner : MonoBehaviour
    {
        public static LevelSpawner inst;
        public GameObject player;

        private int levelPlaying = 2;
        public int LevelPlaying
        {
            set {
                PlayerPrefs.SetInt("StackSix_LevelPlaying", value);
            }
            get
            {
                if (!PlayerPrefs.HasKey("StackSix_LevelPlaying"))
                    return 1;
                return PlayerPrefs.GetInt("StackSix_LevelPlaying");
            }
        }

        public bool spawningLevel = false;
        private void Awake()
        {
            inst = this;
        }
        private void Start()
        {
            SpawnLv(LevelPlaying);
        }
        private void Update()
        {
            levelPlaying = LevelPlaying;
        }
        public void SpawnLv(int level)
        {
            StartCoroutine(IESpawnLV(level));
        }
        IEnumerator IESpawnLV(int level)
        {
            spawningLevel = true;
            foreach (Transform item in transform)
            {
                if (item.gameObject != gameObject && item.gameObject != player)
                    Destroy(item.gameObject);
            }

            player.SetActive(false);

            int nbBlock = GetBlockWithLv(level);

            for (int i = 0; i < nbBlock; i++)
            {
                int rd = (int)Random.Range(0, ConfigColecter.instance.stackSixConfig.blocks.Count);
                GameObject _block = Instantiate(ConfigColecter.instance.stackSixConfig.blocks[rd], transform);
                _block.transform.localPosition = new Vector3(0, i * ConfigColecter.instance.stackSixConfig.disTo2Block, 0);
                yield return null;
            }
            player.transform.localPosition = new Vector3(player.transform.localPosition.x, nbBlock * ConfigColecter.instance.stackSixConfig.disTo2Block, 0);
            player.SetActive(true);
            yield return new WaitForSeconds(1);
            DisplayController.inst.spawningLevelPanel.OnShow(level);

            yield return new WaitForSeconds(3);
            DisplayController.inst.spawningLevelPanel.OnHide();
            spawningLevel = false;
        }
        int GetBlockWithLv(int lv)
        {
            switch (lv)
            {
                case 1: return 2;
                case 2:
                case 3: return 3;
                case 4:
                case 5: 
                case 6: return 4;
                case 7:
                case 8: 
                case 9: return 5;
                case 10: 
                case 11: 
                case 12: return 6;
                case 13: 
                case 14: return 7;
                case 15: return 8;
                case 16: return 9;
                case 17: return 10;
                case 18: return 11;
                case 19: return 12;
                case 20: return 13;

                default: return 14;
            }
        }
    }
}