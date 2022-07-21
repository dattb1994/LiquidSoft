using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiquidSoft
{
    public class SetLiquidPosY : MonoBehaviour
    {
        public Liquid liquid;

        private void Start()
        {
            liquid = GetComponent<Liquid>();
        }
        // Update is called once per frame
        void Update()
        {
            if(liquid.material!=null)
                liquid.material.SetFloat("_PosY", transform.position.y);
        }
    }
}