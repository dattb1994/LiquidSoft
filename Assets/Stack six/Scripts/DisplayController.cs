using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StackSix
{
    public delegate void OnPostEventSingle();
    public class DisplayController : MonoBehaviour
    {
        public static DisplayController inst;

        public LevelSpawningPanel spawningLevelPanel;
        public OnLevelSuccessPanel levelSuccessPanel;
        public GameObject onLostPanel;

        public OnPostEventSingle OnTapToBox;
        public OnPostEventSingle OnLostLevel;
        public OnPostEventSingle OnLevelSuccess;
        public OnPostEventSingle OnUnFreezeAllBox;

        private void Awake()
        {
            inst = this;
        }
    }
}
