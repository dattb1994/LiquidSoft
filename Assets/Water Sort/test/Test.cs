using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiquidSoft
{
    public class Test : MonoBehaviour
    {
        public float vol;
        public int per;
        public float angle;

        public float a, b,c;

        private void Update()
        {
            //angle = WaterSoftExtension.FindA(new Vector2(-1.02f, 2.41f), new Vector2(-67,-90), vol);
            angle = WaterSoftExtension.VolToAngle(vol);
            per = WaterSoftExtension.VolumeToInt(vol);

            transform.GetChild(0).localScale = new Vector3(1 , (float)per/100, 0);

            transform.eulerAngles = new Vector3(0, 0, angle);

            b = WaterSoftExtension.IntToVolume((int)a);
            c = WaterSoftExtension.VolToAngle(b);

        }
    }

}

/*
        _MainColor("Main Color", Color) = (1, 1, 1, 1)

        _Volume("Volume", Range(-5, 6)) = 0.0

        _WaveColor("Wave Color", Color) = (1, 1, 1, 1)

        _WaveSpeed("Wave Speed", Float) = 1.0

        _GlobalWaveSpeed("Global Wave Speed", Float) = 1.0

        _GlobalWaveHeight("Global Wave Height", Float) = 0.1
        [Toggle] _enable("Enable detail", Float) = 1

        _DetailSpeed("Detail Wave Speed", Float) = 1.0

        _DetailDensity("Detail Wave Resolution", Float) = 10.0

        _RimColor("Density Color", Color) = (1, 1, 1, 1)

        _RimPower("Density Power", Range(0, 10)) = 0.0
*/