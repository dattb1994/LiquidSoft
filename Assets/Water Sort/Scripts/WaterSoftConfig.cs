
using UnityEngine;
namespace LiquidSoft
{
    [CreateAssetMenu(fileName = "Water Soft Other", menuName = "ScriptableObjects/WaterSoft/Other", order = 1)]
    public class WaterSoftConfig : ScriptableObject
    {
        public float speedMove = 10;
        public float speedPour = 3;
        public int coutColor = 5;
        public int minBottom=0;
        public int maxTop = 5;
        public float minPer;
        public float maxPer;

        public int perOneParam { get => (int)(100 - minBottom - maxTop) / coutColor; }

    }
}