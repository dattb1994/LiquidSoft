using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LiquidSoft
{
    [ExecuteInEditMode]
    public class CameraFixer : MonoBehaviour
    {
        public bool MaintainWidth = true;
        Vector3 CameraPos;
        float DefaultWidth;
        float DefaultHeight;
        void Start()
        {
            CameraPos = Camera.main.transform.position;

            DefaultWidth = Camera.main.orthographicSize * 1080;
            DefaultHeight = Camera.main.orthographicSize;
            if (MaintainWidth)
            {
                Camera.main.orthographicSize = DefaultWidth / Camera.main.aspect;
            }
            Camera.main.transform.position = new Vector3(CameraPos.x, -1 * (DefaultHeight - Camera.main.orthographicSize), CameraPos.z);
        }


        /* old
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
        */
    }
}
