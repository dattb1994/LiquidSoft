using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StackSix
{
    public class Box : MonoBehaviour
    {
        public List<BrokenBox> brokenBoxes;
        private IEnumerator Start()
        {
            //DisplayController.inst.OnLostLevel += OnLevelLost;
            DisplayController.inst.OnUnFreezeAllBox += UnFreezeAll;
            while (LevelSpawner.inst.spawningLevel)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                yield return null;
            }
        }
        public void UnFreezeAll()
        {
            if (GetComponent<Rigidbody2D>())
            {
                if (inViewCamera())
                {
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                }
                else
                    StartCoroutine(WaitUnFreezeAll());
            }
            DisplayController.inst.OnUnFreezeAllBox -= UnFreezeAll;
        }
        IEnumerator WaitUnFreezeAll()
        {
            while (!inViewCamera())
                yield return null;

            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }

        private void OnMouseDown()
        {
            if (LevelSpawner.inst.spawningLevel || !GPController.instance.canTapToBox || GPController.instance.gameHasLost) return;

            if (!GPController.instance.unFreezeAllBox)
                DisplayController.inst.OnUnFreezeAllBox?.Invoke();

            Destroy(Instantiate(ConfigColecter.instance.stackSixConfig.clickSound,Camera.main.transform),1);

            DisplayController.inst.OnTapToBox.Invoke();
            WhenDestroy();
            
            //Destroy(gameObject);
        }
        private void OnLevelLost()
        {
            if(GetComponent<Rigidbody2D>())
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        bool inViewCamera()
        {
            return Camera.main.IsObjectVisible(GetComponent<SpriteRenderer>());
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "checkLost")
            {
                WhenDestroy();
            }
        }

        private void WhenDestroy()
        {
            foreach (BrokenBox item in GetComponentsInChildren<BrokenBox>())
            {
                item.Play(GetComponent<SpriteRenderer>().color);
            }
            GetComponent<SpriteRenderer>().enabled = false;
            foreach (var item in GetComponents<Collider2D>())
            {
                item.enabled = false;
            }
            transform.GetChild(0).GetComponent<Renderer>().enabled = false;
            //transform.position = new Vector3(Random.Range(1000, 3000), 0, 0);
        }
    }

}
