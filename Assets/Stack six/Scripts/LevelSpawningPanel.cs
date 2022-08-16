using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace StackSix
{
    public class LevelSpawningPanel : MonoBehaviour
    {
        public Text txtLevel;

        public void OnShow(int lv)
        {
            print("show LevelSpawningPanel");
            txtLevel.text = "Level " + lv;// + System.Environment.NewLine + "READY!!!";
            gameObject.SetActive(true);
        }
        public void OnHide()
        {
            gameObject.SetActive(false);
        }
    }
}
