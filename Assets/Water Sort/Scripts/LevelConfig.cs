using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LiquidSoft
{
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/WaterSoft/Level", order = 2)]
    public class LevelConfig : ScriptableObject
    {
        public List<LevelInfo> levels;
        public override string ToString()
        {
            LevelConfogToString lv = new LevelConfogToString(levels);

            return JsonUtility.ToJson(lv);
        }
    }
    public class LevelConfogToString
    {
        public List<LevelInfo> levels=new List<LevelInfo>();
        public LevelConfogToString() { }
        public LevelConfogToString(List<LevelInfo> _levels)
        {
            levels.Clear();
            foreach (var item in _levels)
            {
                levels.Add(item);
            }
        }
    }

}