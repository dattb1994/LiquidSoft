using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StackSix
{
    [ExecuteInEditMode]
    public class _Editor : MonoBehaviour
    {
        // Start is called before the first frame update
        void OnEnable()
        {
            Color c = new Color((float)Random.Range(0, 255) / 255, (float)Random.Range(0, 255) / 255, (float)Random.Range(0, 255) / 255, 1);

            foreach (var item in GetComponentsInChildren<SpriteRenderer>())
            {
                item.color = c;
            }

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
