using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StackSix
{
    public class CameraController : MonoBehaviour
    {
        public float anchorWithGround = 5;
        private void Start()
        {
            targetY = transform.position.y;
        }
        private void Update()
        {
            if (GPData.instance.groundPlaying == null || GPData.instance.poligonPlaying==null) return;

            //print(GPData.instance.GetDisGroundWithPligon());

            if (GPData.instance.GetDisGroundWithPligon() > ConfigColecter.instance.stackSixConfig.minDistanceGroundWithPoligon)
                SetTransformWithY(GPData.instance.poligonPlaying.position.y);
            else
                SetTransformWithY(GPData.instance.groundPlaying.position.y+4);
        }

        float targetY;
        void SetTransformWithY(float _y)
        {
            if (targetY != _y)
                targetY = Mathf.MoveTowards(targetY, _y, ConfigColecter.instance.stackSixConfig.cameraSmoothLerp);
            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        }
    }
    public static class CameraEx
    {
        public static bool IsObjectVisible(this UnityEngine.Camera @this, Renderer renderer)
        {
            return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(@this), renderer.bounds);
        }
    }
}