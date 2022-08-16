using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StackSix
{
    [CreateAssetMenu(fileName = "Stack six config", menuName = "ScriptableObjects/Stack Six/game play config", order = 1)]
    public class StackSixConfig : ScriptableObject
    {
        public float minDistanceGroundWithPoligon = 10;
        public float cameraSmoothLerp = .2f;
        public float disTo2Block = 2.43f;
        public float cdTapToBox = .2f;
        public List<GameObject> blocks = new List<GameObject>();
        public float massPlayer;
        public GameObject clickSound;
    }
}