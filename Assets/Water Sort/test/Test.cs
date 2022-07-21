using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiquidSoft
{
    [ExecuteAlways]
    public class Test : MonoBehaviour
    {
        public Material m;

        private void Update()
        {
            m.SetFloat("_PosY", transform.position.y);
        }
    }

}