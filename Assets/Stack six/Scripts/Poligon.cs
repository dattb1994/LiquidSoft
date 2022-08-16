using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StackSix
{
    public class Poligon : MonoBehaviour
    {
        private Rigidbody2D rigidbody2D;
        private void OnEnable()
        {
            GPData.instance.SetPoligonPlaying(transform);
        }
        private void Start()
        {
            //DisplayController.inst.OnLostLevel += OnLevelLost;
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            rigidbody2D.mass = ConfigColecter.instance.stackSixConfig.massPlayer;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "checkLost")
            {
                print("you lost");
                DisplayController.inst.OnLostLevel?.Invoke();
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<GroundCube>())
            {
                print("you win");
                DisplayController.inst.OnLevelSuccess?.Invoke();
            }
        }
        private void OnLevelLost()
        {
            print(gameObject.name + " lostttttttttt");
            if (GetComponent<Rigidbody2D>())
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}