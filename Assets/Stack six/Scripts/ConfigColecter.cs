using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StackSix
{
    public class ConfigColecter : MonoBehaviour
    {
        public static ConfigColecter instance;
        private void Awake()
        {
            instance = this;
        }
        public StackSixConfig stackSixConfig;
    }
}
