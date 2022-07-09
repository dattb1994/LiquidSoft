using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiquidSoft
{
    public class PosPouring : MonoBehaviour
    {
        public LineRenderer line;

        public Transform t1, t2;
        private void Start()
        {
            line.SetPosition(0, t1.position);
            line.SetPosition(1, t2.position);
            GetComponent<AudioSource>().Stop();
        }

        public void OnStartPour(Color c)
        {
            t1.transform.localPosition = Vector3.zero;
            t2.transform.localPosition = Vector3.zero;

            line.material.color = c;

            line.enabled = true;
            StartCoroutine(Pouring());

        }
        IEnumerator Pouring()
        {
            float y = 0;
            GetComponent<AudioSource>().Play();
            while (y > -12)
            {
                y -= Time.deltaTime * 50;
                t2.localPosition = new Vector3(0,y,0);

                line.SetPosition(0, t1.position);
                line.SetPosition(1, t2.position);
                yield return null;
            }
        }

        public void OnEndPour()
        {
            StartCoroutine(EndPouring());
        }
        IEnumerator EndPouring()
        {
            float y = 0;
            while (y > -12)
            {
                y -= Time.deltaTime * 60;
                t1.localPosition = new Vector3(0, y, 0);

                line.SetPosition(0, t1.position);
                line.SetPosition(1, t2.position);
                yield return null;
            }
            GetComponent<AudioSource>().Stop();
            line.enabled = false;
        }
    }
}