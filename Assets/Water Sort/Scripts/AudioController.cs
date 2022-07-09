using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiquidSoft
{
    public class AudioController : MonoBehaviour
    {
        public static AudioController instance;

        public List<GameObject> sounds;

        public GameObject sound_ground;
        public GameObject sound_level_done;

        public float volume = 0;

        public void SpawnAudio(int index)
        {
            Destroy(Instantiate(sounds[index],null), sounds[index].GetComponent<AudioSource>().clip.length);
        }

        public bool soundIsOn { set => PlayerPrefs.SetString("soundIsOn", value == true ? "true" : "false");
            get
            {
                return PlayerPrefs.GetString("soundIsOn") == "true" ? true : false;
            }
        }
        private void Update()
        {
            volume = AudioListener.volume;
        }
        public OnPostEvent OnTurnOnSound;
        public OnPostEvent OnTurnOffSound;

        private void Awake()
        {
            instance = this;
        }

        private IEnumerator Start()
        {
            EventListenner.instance.OnLevelIsSpawn += _OnLevelWasLoaded;
            EventListenner.instance.OnLevelDone += _OnLevelDone;

            yield return new WaitForSeconds(.5f);

            if (!PlayerPrefs.HasKey("soundIsOn"))
                soundIsOn = true;

            if (soundIsOn)
                TurnOn();
            else
                TurnOff();

        }
        public void _OnLevelWasLoaded(int level)
        {
            sound_ground.SetActive(true);
            sound_level_done.SetActive(false);
        }
        public void _OnLevelDone(int lv)
        {
            sound_ground.SetActive(false);
            sound_level_done.SetActive(true);
        }
        public void OnSoundChange()
        {
            if (soundIsOn)
            {
                TurnOff();
            }
            else
                TurnOn();
        }

        public void TurnOn()
        {
            print("TurnOn");
            soundIsOn = true;
            AudioListener.volume = 1;
            OnTurnOnSound?.Invoke();
        }
        public void TurnOff()
        {
            print("TurnOff");
            soundIsOn = false;
            AudioListener.volume = 0;
            OnTurnOffSound?.Invoke();
        }
    }
}