using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LiquidSoft
{
    [ExecuteInEditMode]
    public class CameraFixer : MonoBehaviour
    {
        // Start is called before the first frame update

        public float ratio = 1.5f;
        void Start()
        {
            //GetComponent<Camera>().rect = new Rect(0, 0.25f, 1, (float)Screen.width / Screen.height);
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

            //GetComponent<Camera>().rect = new Rect(0, 0.25f, 1* ratio, (float)Screen.width * ratio / Screen.height);

            //GetComponent<Camera>().orthographicSize = WaterSoftExtension.FindFloat((float)720/1600, (float)1440 / 2560, 53.2f, 44.18f, (float)(Screen.width/Screen.height));
        }
    }
}
// 0.5625       0.45 
//  720     54.19 x
//  1080    42.2  a


//  720/1600  ~  52.5

//  1 ~ 25.6

//  1440/2560   44.18
