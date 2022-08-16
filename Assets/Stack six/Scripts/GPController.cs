using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace StackSix
{
    public class GPController : MonoBehaviour
    {
        public static GPController instance;
        public GPData myData;

        public bool canTapToBox = false;
        public bool unFreezeAllBox = false;
        public bool gameHasLost = false;
        private float countTimeTapToBox = 0;

        public GameObject bgSound;

        private void Awake()
        {
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            myData = GPData.instance;
            DisplayController.inst.OnLostLevel += OnLost;
            DisplayController.inst.OnTapToBox += SetCantTapToBox;
            DisplayController.inst.OnLevelSuccess += OnNextLevel;
            DisplayController.inst.OnUnFreezeAllBox += OnUnFreezeAllBox;
        }
        private void Update()
        {
            if (!canTapToBox)
            {
                countTimeTapToBox += Time.deltaTime;
                if (countTimeTapToBox >= ConfigColecter.instance.stackSixConfig.cdTapToBox)
                    canTapToBox = true;
            }

            //if(Input.GetKeyDown(KeyCode.R))


        }
        public void OnLost()
        {
            DisplayController.inst.onLostPanel.SetActive(true);
            bgSound.SetActive(false);
            gameHasLost = true;
        }
        public void OnPlayAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void OnNextLevel()
        {
            LevelSpawner.inst.LevelPlaying++;
            DisplayController.inst.levelSuccessPanel.OnShowPanel();
            bgSound.SetActive(false);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private void SetCantTapToBox()
        {
            canTapToBox = false;
            countTimeTapToBox = 0;
        }
        private void OnUnFreezeAllBox()
        {
            unFreezeAllBox = true;
            DisplayController.inst.OnUnFreezeAllBox -= OnUnFreezeAllBox;
            bgSound.SetActive(true);
        }
    }
}
