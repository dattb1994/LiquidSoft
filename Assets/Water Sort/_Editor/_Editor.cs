using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiquidSoft
{
    [ExecuteInEditMode]
    public class _Editor : MonoBehaviour
    {
        public LevelInfo levelInfo;

        public LevelConfig levelConfig;

        public bool action = false;
        public bool resetLevel = false;
        public bool printColor = false;


        public Color color;
        public string StrColor;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (action)
            {
                OnAction();
            }

            if (resetLevel)
            {
                levelInfo = new LevelInfo();
                resetLevel = false;
            }

            if (printColor)
            {
                StrColor = color.r + "f," + color.g + "f," + color.b + "f,1";
                printColor = false;
            }
        }
        void OnAction()
        {
            levelConfig.levels.Add(levelInfo);


            action = false;
        }
    }
}