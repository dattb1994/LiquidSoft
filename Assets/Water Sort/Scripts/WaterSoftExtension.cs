using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiquidSoft
{
    public static class WaterSoftExtension
    {
        public static void SetLiqidShader(Material mat, LiquidShaderInfo info)
        {
            mat.SetColor("_MainColor", info._MainColor);
            mat.SetFloat("_Volume", info._Volume);
            mat.SetColor("_WaveColor", info._WaveColor);
            mat.SetFloat("_WaveSpeed", info._WaveSpeed);
            mat.SetFloat("_GlobalWaveSpeed", info._GlobalWaveSpeed);
            mat.SetFloat("_DetailDensity", info._DetailDensity);
            mat.SetFloat("_RimPower", info._RimPower);
            mat.SetColor("_RimColor", info._RimColor);
            mat.SetFloat("_DetailSpeed", info._DetailSpeed);
            mat.SetFloat("_GlobalWaveHeight", info._GlobalWaveHeight);
        }
        public static float IntToVolume(int per)
        {
            float max = ConfigColecter.instance.softConfig.maxPer;
            float min = ConfigColecter.instance.softConfig.minPer;

            return FindFloat(100 ,0, min, max, per);
        }
        public static float FloatToVolume(float per)
        {
            float max = ConfigColecter.instance.softConfig.maxPer;
            float min = ConfigColecter.instance.softConfig.minPer;

            return FindFloat(100, 0, min, max, per);
        }
        public static int VolumeToInt(float vol)
        {
            float max = ConfigColecter.instance.softConfig.maxPer;
            float min = ConfigColecter.instance.softConfig.minPer;

            return (int)FindFloat( min, max, 100, 0, vol);
            
        }

        public static float IntToAngle(int per)
        {
            float vol = IntToVolume(per);
            return VolToAngle(vol);

            // 58 ~ -77
        }

        public static float FindFloat(float x1, float x2, float a1, float a2, float _value)
        {
            float result = (float)(_value - x1) * (a2-a1) / (x2-x1) + a1;
            return (float)result;

        }

        public static float VolToAngle(float vol)
        {
            float result = 0;

            float x1 = IntToVolume(60);
            float x2 = IntToVolume(80);

            float a2 = -36.091f;
            float a1 = -62.504f;

            result = FindFloat(x1,x2,a1,a2,vol) ;

            if (result > -90) return result;

            return -90;
        }


        public static Color EnumToColor(TypeColor type)
        {
            switch (type)
            {
                case TypeColor.red: return Color.red;
                case TypeColor.blue: return Color.blue;
                case TypeColor.yellow: return Color.yellow;
                case TypeColor.green: return Color.green;
                case TypeColor.pink: return new Color(0.9675527f, 0.433f, 0.9803922f, 1);
                case TypeColor.violet: return new Color(0.39f, 0f, 1f, 1);
                case TypeColor.orange: return new Color(1f, 0.3087484f, 0f, 1);
                case TypeColor.grey: return new Color(0.5849056f, 0.5849056f, 0.5849056f, 1);
                case TypeColor.indigo_blue: return new Color(0.4622642f, 0.1504539f, 0.3022301f, 1);
                case TypeColor.dark_green: return new Color(0f, 0.05322438f, 0.6603774f, 1);
                case TypeColor.light_blue: return new Color(0.6048742f, 1f, 0.2311321f, 1);
                default: return Color.white;
            }
        }
        public static void LiquidWaveing(Material _liquid)
        {
            Debug.Log(_liquid);
            _liquid.SetFloat("_GlobalWaveSpeed", .2f);
            _liquid.SetFloat("_GlobalWaveHeight", .2f);
            _liquid.SetFloat("_DetailSpeed", 3);
            _liquid.SetFloat("_DetailDensity", 4);
        }
        public static void LiquidDontWaveing(Material _liquid)
        {
            Debug.Log(_liquid);
            _liquid.SetFloat("_GlobalWaveSpeed", 0);
            _liquid.SetFloat("_GlobalWaveHeight", 0);
            _liquid.SetFloat("_DetailSpeed", 0);
            _liquid.SetFloat("_DetailDensity", 0);
        }
    }
}
