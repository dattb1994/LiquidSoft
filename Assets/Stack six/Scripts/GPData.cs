using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StackSix
{
    public class GPData : MonoBehaviour
    {
        public static GPData instance;
        private void Awake() => instance = this;
        public Transform groundPlaying;
        public void SetGroundPlaying(Transform _t) => groundPlaying = _t;

        public Transform poligonPlaying;
        public void SetPoligonPlaying(Transform _t) => poligonPlaying = _t;

        public float GetDisGroundWithPligon() => Vector3.Distance(groundPlaying.position, poligonPlaying.position);
        
    }
}
