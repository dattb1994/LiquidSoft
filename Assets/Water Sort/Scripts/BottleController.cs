using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LiquidSoft
{
    public class BottleController : MonoBehaviour
    {
        public List<LiquidLevel> levelsInit;

        public GameObject liquidPrefab;

        private Vector3 startPos;

        public Transform posPour;

        public TextMeshPro textMesh;

        public GameObject effWhenOke;
        private void Start()
        {
            for (int i = 0; i < levelsInit.Count; i++)
            {
                SpawnLiquid(levelsInit[i],i);
            }
            startPos = transform.parent.position;

            effWhenOke.SetActive(false);
        }
        private void Update()
        {
            textMesh.text = GetTopPercent() + "";
        }
        public void SpawnLiquid(LiquidLevel level, int z)
        {
            Liquid _liqid = Instantiate(liquidPrefab, transform).GetComponent<Liquid>();
            _liqid.gameObject.name = "liquid_" + level.color + "_" + level.percent;
            _liqid.transform.localPosition = new Vector3(0,0,z);
            _liqid.liquidLevel = new LiquidLevel(level);
            _liqid.SetMaterial();

        }

        public void PourOut(PosPouring pouring)
        {
            if (transform.childCount == 0)
            {
                OnUnSelected();
                GPController.instance.bottleSelecting = null;
                return;
            }
            StartCoroutine(PouringOut(pouring));
        }
        IEnumerator PouringOut(PosPouring pouring)
        {
            GPController.instance.pouring = true;

            float minPer = 0;
            float _per = transform.GetChild(transform.childCount - 1).GetComponent<Liquid>().liquidLevel.percent;

            if (transform.childCount >= 2)
                minPer = transform.GetChild(transform.childCount - 2).GetComponent<Liquid>().liquidLevel.percent;
            ///di chuyen den vi trí đổ
            while (transform.parent.transform.position != pouring.transform.position)
            {
                transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position, pouring.transform.position, Time.deltaTime * ConfigColecter.instance.softConfig.speedMove);
                yield return null;
            }

            ///Xoay góc đổ
            //float angle = WaterSoftExtension.IntToAngle(GetTopPercent());
            //print("Goc do : " + WaterSoftExtension.IntToAngle(GetTopPercent()));

            //float angleZ = 0;

            //while (angleZ != angle)
            //{
            //    angleZ = Mathf.MoveTowards(angleZ, angle, Time.deltaTime * ConfigColecter.instance.softConfig.speedPour * 3);
            //    //print(transform.parent.eulerAngles +"_____"+ angle);
            //    transform.parent.eulerAngles = new Vector3(0, 0, angleZ);

            //    yield return null;
            //}
            transform.parent.eulerAngles = new Vector3(0, 0, WaterSoftExtension.IntToAngle(GetTopPercent()));

            ///Bắt đầu đổ
            pouring.OnStartPour(WaterSoftExtension.EnumToColor(GetTopLiquid().liquidLevel.color));

            LiquidLevel liquidLevelIn = new LiquidLevel();
            liquidLevelIn.color = GetTopLiquidLevel().color;
            liquidLevelIn.percent = GetTopCountPercent();
            GPController.instance.bottlePourIn.PourIn(liquidLevelIn);

            /// bat dau do
            while (_per > minPer)
            {
                _per = Mathf.MoveTowards(_per, minPer, Time.deltaTime * ConfigColecter.instance.softConfig.speedPour);

                //if(WaterSoftExtension.IntToAngle((int)_per)>=-90)
                transform.parent.eulerAngles = new Vector3(0, 0, WaterSoftExtension.IntToAngle((int)_per));

                if(GetTopPercent()>40)
                    GetTopLiquid().SetVolume(WaterSoftExtension.IntToVolume((int)_per));

                //if ()

                yield return null;
            }
            GetTopLiquid().SetVolume(WaterSoftExtension.IntToVolume((int)_per));

            pouring.OnEndPour();
            transform.parent.eulerAngles = new Vector3(0, 0, 0);
            Destroy(transform.GetChild(transform.childCount - 1).gameObject);

            /// ve lai vi tri ban dau
            while (transform.parent.transform.position != startPos)
            {
                transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position, startPos, Time.deltaTime * ConfigColecter.instance.softConfig.speedMove);
                yield return null;
            }
            ///  
            GPController.instance.bottleSelecting = null;


            GPController.instance.pouring = false;
            OnPourInDone();

            CheckWhenEndPourIn(GPController.instance.bottlePourInlast);

        }
        IEnumerator PouringOut1(PosPouring pouring)
        {
            GPController.instance.pouring = true;

            float minPer = 0;
            float _per = transform.GetChild(transform.childCount - 1).GetComponent<Liquid>().liquidLevel.percent;

            if (transform.childCount >= 2)
                minPer = transform.GetChild(transform.childCount - 2).GetComponent<Liquid>().liquidLevel.percent;

            /// dich chuyen den vi tri do
            while (transform.parent.transform.position != pouring.transform.position)
            {
                transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position, pouring.transform.position, Time.deltaTime * ConfigColecter.instance.softConfig.speedMove);
                yield return null;
            }

            /// xoay goc do
            /// 
            float _per1 = 100;
            while (_per1 > _per)
            {
                _per1 -= Time.deltaTime * ConfigColecter.instance.softConfig.speedPour*3;
                transform.parent.eulerAngles = new Vector3(0, 0, WaterSoftExtension.IntToAngle((int)_per1));

                yield return null;
            }

            pouring.OnStartPour(WaterSoftExtension.EnumToColor(GetTopLiquid().liquidLevel.color));

            LiquidLevel liquidLevelIn = new LiquidLevel();
            liquidLevelIn.color = GetTopLiquidLevel().color;
            liquidLevelIn.percent = GetTopCountPercent();
            GPController.instance.bottlePourIn.PourIn(liquidLevelIn);
            /// bat dau do
            while (_per > minPer)
            {
                _per = Mathf.MoveTowards(_per, minPer, Time.deltaTime * ConfigColecter.instance.softConfig.speedPour);

                transform.parent.eulerAngles = new Vector3(0, 0, WaterSoftExtension.IntToAngle((int)_per));
                GetTopLiquid().SetVolume(WaterSoftExtension.IntToVolume((int)_per));

                //if ()

                yield return null;
            }
            pouring.OnEndPour();
            transform.parent.eulerAngles = new Vector3(0, 0, 0);
            Destroy(transform.GetChild(transform.childCount - 1).gameObject);

            /// ve lai vi tri ban dau
            while (transform.parent.transform.position != startPos)
            {
                transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position, startPos, Time.deltaTime * ConfigColecter.instance.softConfig.speedMove);
                yield return null;
            }
            ///  
            GPController.instance.bottleSelecting = null;


            GPController.instance.pouring = false;
            OnPourInDone();
        }

        public void PourIn(LiquidLevel liquidLevel)
        {

            if(transform.childCount==0)
                StartCoroutine(PouringInDonotChild(liquidLevel));
            else
                StartCoroutine(PouringInHaveChild(liquidLevel));
        }

        IEnumerator PouringInDonotChild(LiquidLevel liquidLevel)
        {
            Liquid newLiquid = Instantiate(liquidPrefab, transform).GetComponent<Liquid>();

            newLiquid.Waving();

            newLiquid.transform.localPosition = Vector3.zero;

            LiquidLevel newLv = new LiquidLevel();
            newLv.percent = 0;
            newLv.color = liquidLevel.color;

            newLiquid.liquidLevel = new LiquidLevel(newLv);
            newLiquid.SetMaterial();

            int newTop = liquidLevel.percent + ConfigColecter.instance.softConfig.minBottom;
            float per = 0;


            while (per < newTop)
            {
                if (per < newTop)
                    per = Mathf.MoveTowards(per, newTop, Time.deltaTime * ConfigColecter.instance.softConfig.speedPour);
                else
                    per = newTop;
                newLiquid.SetVolume(WaterSoftExtension.IntToVolume((int)per));
                yield return null;

            }
            newLiquid.EndWaving();

            //Invoke("CheckWhenEndPourIn", .1f);
        }
        IEnumerator PouringInHaveChild(LiquidLevel liquidLevel)
        {
            GetTopLiquid().Waving();

            int newTop = GetTopPercent() + liquidLevel.percent;
            float per = GetTopPercent();

            while (per < newTop)
            {
                //per += Time.deltaTime * ConfigColecter.instance.softConfig.speedPour;

                if (per < newTop)
                    per = Mathf.MoveTowards(per, newTop, Time.deltaTime * ConfigColecter.instance.softConfig.speedPour);
                else
                    per = newTop;

                GetTopLiquidObj().GetComponent<Liquid>().SetVolume(WaterSoftExtension.FloatToVolume(per));
                yield return null;
            }
            per = newTop;
            GetTopLiquid().SetVolume(WaterSoftExtension.FloatToVolume(per));

            GetTopLiquid().EndWaving();

            //Invoke("CheckWhenEndPourIn", .1f);

        }
        public void OnPourInDone()
        { 
            GPController.instance.CheckLevelDone();
            GPController.instance.bottlePourIn = null;
        }
        public void CheckWhenEndPourIn(BottleController bottle)
        {
            print("CheckWhenEndPourIn");
            if (!GPController.instance.BottleOke(bottle)) return;

            bottle.effWhenOke.SetActive(false);
            StartCoroutine(IEShowEffWhenOke(bottle));
        }
        IEnumerator IEShowEffWhenOke(BottleController bottle)
        {
            yield return null;
            AudioController.instance.SpawnAudio(1);
            bottle.effWhenOke.SetActive(true);
        }

        /// <summary>
        /// trả về tổng số per của top liqid
        /// </summary>
        /// <returns></returns>
        public int GetTopCountPercent()
        {
            int perTop = GetTopPercent();
            int perBot = 0;

            if (transform.childCount < 2)
                perBot = 0;
            else
                perBot = transform.GetChild(transform.childCount - 2).GetComponent<Liquid>().liquidLevel.percent;


            return perTop - perBot;
        }

        /// <summary>
        /// trả về tổng số per của bottle
        /// </summary>
        /// <returns></returns>
        public int GetTopPercent()
        {
            if (transform.childCount > 0)
                return transform.GetChild(transform.childCount-1).GetComponent<Liquid>().liquidLevel.percent;

            return 0;
        }
        public LiquidLevel GetTopLiquidLevel()
        {
            if (transform.childCount > 0)
                return new LiquidLevel(transform.GetChild(transform.childCount - 1).GetComponent<Liquid>().liquidLevel);

            return new LiquidLevel();
        }
        public GameObject GetTopLiquidObj()
        {
            if (transform.childCount > 0)
                return transform.GetChild(transform.childCount - 1).gameObject;
            return null;
        }
        public Liquid GetTopLiquid()
        {
            if (transform.childCount > 0)
                return transform.GetChild(transform.childCount - 1).GetComponent<Liquid>();

            return null;
        }
        private void OnMouseDown()
        {
            if (GPController.instance.pouring) return;
            GPController.instance.OnMouseClickBottle(this);
        }
        
        public void OnSelected()
        {
            transform.parent.position += new Vector3(0, 1, 0);
            AudioController.instance.SpawnAudio(0);
        }
        public void OnUnSelected()
        {
            transform.parent.position = startPos;
            AudioController.instance.SpawnAudio(0);
        }
    }

    public class StackLiquid
    { 
        //public List<Ob>
    }
    [System.Serializable]
    public class LiquidShaderInfo
    {
        public Color _MainColor = Color.white;
        public float _Volume = 0;
        public Color _WaveColor = Color.white;
        public float _WaveSpeed = 0;
        public float _GlobalWaveSpeed = 0;
        public float _DetailSpeed = 0;
        public float _DetailDensity = 0;
        public float _RimPower = 0;
        public Color _RimColor = Color.white;
        public float _GlobalWaveHeight = 0;
        public LiquidShaderInfo() { }
        public LiquidShaderInfo(float volume)
        {
            _Volume = volume;
        }
    }
}
