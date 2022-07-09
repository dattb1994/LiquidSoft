using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiquidSoft
{
    public class GPController : MonoBehaviour
    {
        public static GPController instance;

        public BottleController bottleSelecting = null;
        public BottleController bottlePourIn = null;

        public BottleController bottlePourInlast = null;

        public Transform levelPlaying = null;

        public bool pouring;

        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            print(ConfigColecter.instance.softConfig.perOneParam);
        }

        public void OnMouseClickBottle(BottleController bottle)
        {
            if (bottleSelecting == null)
            {
                bottleSelecting = bottle;
                bottle.OnSelected();
            }
            else
            {
                if (bottleSelecting == bottle)
                {
                    bottleSelecting = null;
                    bottle.OnUnSelected();

                }
                else
                {
                    if (CheckCanPour(bottle))
                        Pour(bottle);
                    else
                        bottleSelecting.OnUnSelected();
                    bottleSelecting = null;
                }
            }
        }

        public void Pour(BottleController bottle)
        {
            bottleSelecting.PourOut(bottle.posPour.GetComponent<PosPouring>());
            bottlePourIn = bottle;
            bottlePourInlast = bottle;
        }
        public bool CheckCanPour(BottleController bottle)
        {
            if (bottleSelecting.GetTopLiquid() == null) return false;

            if (bottle.GetTopPercent() >= 100 - ConfigColecter.instance.softConfig.minBottom - ConfigColecter.instance.softConfig.maxTop) return false;

            if (bottle.GetTopLiquid() != null)
            {
                if (bottle.GetTopLiquid().liquidLevel.color != bottleSelecting.GetTopLiquid().liquidLevel.color)
                    return false;
                else
                {
                    float cout = bottleSelecting.GetTopCountPercent() + bottle.GetTopPercent();
                    if (100 - ConfigColecter.instance.softConfig.minBottom - cout < 0)
                        return false;
                }
            }
            return true;
        }

        public void SetLevelPlaying(Transform _lv)
        {
            levelPlaying = _lv;
        }

        #region Check level done
        public List<TypeColor> liquids = new List<TypeColor>();
        public List<BottleController> bottles = new List<BottleController>();
        private int cout = 0;
        public void CheckLevelDone()
        {
            Invoke("CheckingLevelDone", .5f);
        }
        public bool BottleOke(BottleController bottle)
        {
            print(11111111);
            if (bottle.transform.childCount != 1)
            {

                print(222222222);
                return false;
            }

            List<BottleController> _bottles = new List<BottleController>();
            foreach (var item in levelPlaying.GetComponentsInChildren<BottleController>())
            {
                _bottles.Add(item);
                print(item);
            }

            //int c = 0;
            TypeColor color = bottle.GetTopLiquid().liquidLevel.color;
            for (int i = 0; i < _bottles.Count; i++)
            {
                if (_bottles[i] != bottle)
                {
                    foreach (Liquid item in _bottles[i].transform.GetComponentsInChildren<Liquid>())
                    {
                        print(item.liquidLevel.color+"____"+ color);
                        if (item.liquidLevel.color == color)
                            return false;
                    }
                }
            }

            return true;
        }
        private void CheckingLevelDone()
        {

            liquids.Clear();
            bottles.Clear();

            if (bottles.Count == 0)
                foreach (var item in levelPlaying.GetComponentsInChildren<BottleController>())
                {
                    bottles.Add(item);
                }

            for (int i = 0; i < bottles.Count; i++)
            {
                if (bottles[i].transform.childCount > 1)
                    return;
            }

            for (int i = 0; i < bottles.Count; i++)
            {
                if (bottles[i].transform.childCount > 0)
                    liquids.Add(bottles[i].transform.GetChild(0).GetComponent<Liquid>().liquidLevel.color);
            }

            for (int j = 0; j < liquids.Count -1; j++)
            {
                for (int k = 1; k < liquids.Count; k++)
                {
                    if (j != k && liquids[j] == liquids[k])
                        return;
                }
            }

            print("on level done");
            EventListenner.instance.OnLevelDone.Invoke(LevelController.instance.levelNow);
        }
        #endregion
    }
}
