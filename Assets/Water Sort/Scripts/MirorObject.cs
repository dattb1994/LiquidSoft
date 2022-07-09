using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LiquidSoft
{
    public class MirorObject : MonoBehaviour
    {
        public GameObject m_my;
        private void OnEnable()
        {
            m_my.SetActive(true);
        }
        private void OnDisable()
        {
            m_my.SetActive(false);
        }
    }
}
