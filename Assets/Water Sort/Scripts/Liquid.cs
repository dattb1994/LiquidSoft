using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiquidSoft
{
    public class Liquid : MonoBehaviour
    {
        public Material material;
        public LiquidLevel liquidLevel;

        public Shader liquidShader;

        private void Start()
        {
            SetMaterial();
        }

        public void SetMaterial()
        {
            Material _material = new Material(liquidShader);
            WaterSoftExtension.SetLiqidShader(_material, new LiquidShaderInfo());
            GetComponent<Renderer>().material = _material;
            material = GetComponent<Renderer>().material;
            SetData(liquidLevel);
        }
        public void SetVolume(float vl)
        {
            //print(gameObject.name+"____" +vl);
            material.SetFloat("_Volume", vl);
            liquidLevel.percent = WaterSoftExtension.VolumeToInt(vl);
        }
        public void SetData(LiquidLevel liquid)
        {
            liquidLevel = new LiquidLevel(liquid);
            material.SetColor("_MainColor", WaterSoftExtension.EnumToColor(liquid.color));

            material.SetFloat("_Volume", WaterSoftExtension.IntToVolume(liquidLevel.percent));
        }
        public void Waving()
        {
            WaterSoftExtension.LiquidWaveing(GetComponent<Renderer>().material);
        }
        public void EndWaving()
        {
            WaterSoftExtension.LiquidDontWaveing(GetComponent<Renderer>().material);
        }
    }

    [System.Serializable]
    public class LiquidLevel
    {
        public TypeColor color = TypeColor.red;

        //[Range(0, 100)]
        public int percent { set; get; }

        public int amount = 1;

        public LiquidLevel() { }
        public LiquidLevel(LiquidLevel liquidLevel)
        {
            color = liquidLevel.color;
            percent = liquidLevel.percent;
            amount = liquidLevel.amount;

            if (liquidLevel.amount > 0) percent = amoutToPercent();

            else
                percent = liquidLevel.percent;
        }

        /// <summary>
        /// phan tram cua so tang
        /// </summary>
        /// <returns></returns>
        public int amoutToPercent()
        {
            return ConfigColecter.instance.softConfig.minBottom + ConfigColecter.instance.softConfig.perOneParam * amount;
        }
    }
    [System.Serializable]
    public enum TypeColor
    {
        red = 1,
        blue = 2,
        yellow = 3,
        green = 4,
        pink =5,
        violet=6,
        orange=7,
        grey=8,
        indigo_blue=9,
        dark_green = 10,
        light_blue = 11
    }

}
