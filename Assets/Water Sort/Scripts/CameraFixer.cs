using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LiquidSoft
{
    [ExecuteInEditMode]
    public class CameraFixer : MonoBehaviour
    {

        
        public float ratio = 1.5f;
        void Start()
        {
            if (Screen.width == 1080 && Screen.height == 2610)
                GetComponent<Camera>().orthographicSize = 69.1f;
            else
                GetComponent<Camera>().orthographicSize = 54.45f;
        }
        private void Update()
        {
            if (Screen.width == 1080 && Screen.height == 2610)
                GetComponent<Camera>().orthographicSize = 59.6f;
            else
                GetComponent<Camera>().orthographicSize = 53f;

        }

    }
}
