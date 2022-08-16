using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StackSix
{
    public class GroundCube : MonoBehaviour
    {
        private void Start ()
        {
            GPData.instance.SetGroundPlaying(transform);
        }
    }
}