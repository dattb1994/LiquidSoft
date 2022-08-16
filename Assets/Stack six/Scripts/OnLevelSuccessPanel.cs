using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace StackSix
{
    public class OnLevelSuccessPanel : MonoBehaviour
    {
        public Text txtLevel;
        public Transform imgSucces;
        public void OnShowPanel()
        {
            imgSucces.transform.localScale = Vector3.zero;
            int lv = LevelSpawner.inst.LevelPlaying - 1;
            //txtLevel.text = "SUCCESS";//; + System.Environment.NewLine + "LEVEL " + lv;

            gameObject.SetActive(true);
            StartCoroutine(Showing());
        }

        private IEnumerator Showing()
        {
            while (imgSucces.localScale.x < 1)
            {
                imgSucces.localScale += new Vector3(1f, 1f, 1f) * Time.deltaTime;
                yield return null;
            }
        }
    }
}
