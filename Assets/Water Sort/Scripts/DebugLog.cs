using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LiquidSoft
{
    public class DebugLog : MonoBehaviour
    {
        public static DebugLog instance;

        public bool canDebug = false;
        private void Awake()
        {
            instance = this;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                Add("AAAAAAAAAAAAAAAA");
            if (Input.GetKeyDown(KeyCode.B))
                Add("BBBBBBBBBBBBBBBBBBBBBBB");
        }
        public void Add(string mesage)
        {
            GetComponent<Text>().text = "";
            if (!canDebug) return;
            GetComponent<Text>().text = "....." + mesage + System.Environment.NewLine + GetComponent<Text>().text;
        }
    }
}
