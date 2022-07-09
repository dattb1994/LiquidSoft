using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LiquidSoft
{
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/WaterSoft/Level", order = 2)]
    public class LevelConfig : ScriptableObject
    {
        public List<LevelInfo> levels;

    }
}