using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace LiquidSoft
{
    public class DisplayController : MonoBehaviour
    {
        public static DisplayController instance;
        public Image imgAudio;
        public Sprite soundOn, soundOff;
        public Text txtLevel;
        public Text txtLevelDone;

        public GameObject levelDonePanel;
        public GameObject playingDisplay;
        public GameObject loadingPanel;
        public GameObject levelIsOverPanel;

        public GameObject timeOuInternetMesage;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            AudioController.instance.OnTurnOnSound += OnTurnOnSound;

            AudioController.instance.OnTurnOffSound += OnTurnOffSound;

            EventListenner.instance.OnLevelDone += OnLevelDone;
            EventListenner.instance.OnLevelIsSpawn += OnLevelPlaying;
            EventListenner.instance.OnLevelLoadStardDone += OnLoadingStaredDone;
            EventListenner.instance.OnTimeOutConnectInternet += WhenTimeOutInternet;
            EventListenner.instance.OnLevelIsOver += OnLevelIsOver;
        }
        public void OnButtonReplayClick()
        {
            //LevelController.instance.OnSpawnLevel(LevelController.instance.levelNow);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void ChangeTextLevel(int _level)
        {
            txtLevel.text = "Level " + _level;
        }
        public void ButtonBackClick()
        {

        }
        public void OnButtonSoundClick()
        {
            AudioController.instance.OnSoundChange();
        }
        public void OnLevelDone(int lv)
        {
            playingDisplay.SetActive(false);
            levelDonePanel.SetActive(true);
            txtLevelDone.text = "Level " + (lv < 10 ? "0" : "") + lv;

        }
        public void OnLevelPlaying(int _lv)
        {
            playingDisplay.SetActive(true);
            levelDonePanel.SetActive(false);
        }

        public void OnTurnOnSound()
        {
            imgAudio.sprite = soundOn;
        }
        public void OnTurnOffSound()
        {
            imgAudio.sprite = soundOff;
        }
        public void OnLoadingStaredDone()
        {
            loadingPanel.SetActive(false);
        }
        public void OnStartLoadingLevel()
        {
            loadingPanel.SetActive(true);
            playingDisplay.SetActive(false);
        }
        public void WhenTimeOutInternet()
        {
            timeOuInternetMesage.SetActive(true);
            Invoke("HideTimeOutInternetMesage", 3);
        }
        private void HideTimeOutInternetMesage()
        {
            timeOuInternetMesage.SetActive(false);
        }
        public void OnLevelIsOver()
        {
            levelIsOverPanel.SetActive(true);
        }
    }
}