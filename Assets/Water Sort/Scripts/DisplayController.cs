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
    }
}