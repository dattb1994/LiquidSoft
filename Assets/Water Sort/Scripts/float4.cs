namespace LiquidSoft
{
    [System.Serializable]
    public class Float4
    {
        public float x1, x2, a1, a2;
        public Float4() { }
        public Float4(float _x1, float _x2, float _a1, float _a2)
        {
            x1 = _x1;
            x2 = _x2;
            a1 = _a1;
            a2 = _a2;
        }
        public float FindX(float a)
        { 
            return  (float)(a - x1) * (a2 - a1) / (x2 - x1) + a1;
        }
    }
}
///  x-x1   a-a1                (a-a1)(x2-x1)
///  ____ = ____    <=>    x =  _____________  + x1
///  x2-x1  a2 -a1                  a2-a1
