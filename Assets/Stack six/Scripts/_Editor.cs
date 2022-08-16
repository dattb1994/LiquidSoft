using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StackSix
{
    [ExecuteInEditMode]
    public class _Editor : MonoBehaviour
    {
        public List<GameObject> prefabs;



        public List<Sprite> sprites;

        public bool active = false;

        // Start is called before the first frame update
        void OnEnable()
        {
            //Color c = new Color((float)Random.Range(0, 255) / 255, (float)Random.Range(0, 255) / 255, (float)Random.Range(0, 255) / 255, 1);

            //foreach (var item in GetComponentsInChildren<SpriteRenderer>())
            //{
            //    item.color = c;
            //}

        }

        // Update is called once per frame
        void Update()
        {
            if (active)
            {
                PlayerPrefs.DeleteKey("StackSix_LevelPlaying");
                //for (int i = 0; i < transform.childCount; i++)
                {
                    //Color c = new Color((float)Random.Range(0, 255) / 255, (float)Random.Range(0, 255) / 255, (float)Random.Range(0, 255) / 255, 1);

                    //transform.GetChild(i).GetComponent<SpriteRenderer>().color = c;
                    //transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().color = c;


                    //GameObject o = Instantiate(new GameObject(gameObject.name+" (Clone)"), transform.GetChild(i).transform);
                    //o.transform.localPosition = Vector3.zero;
                    //o.transform.localEulerAngles = Vector3.zero;
                    //o.transform.localScale = Vector3.zero;

                    //o.AddComponent<SpriteRenderer>().sprite = sprites[i];

                    //transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
                    //transform.GetChild(i).GetChild(0).localPosition = new Vector3(0, 0, -1);

                }


                active = false;
            }
        }
    }
}
