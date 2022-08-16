using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBox : MonoBehaviour
{
    public void Play(Color c)
    {
        transform.SetParent(null);
        GetComponent<ParticleSystem>().startColor = c;
        GetComponent<ParticleSystem>().Play();

        print("broken box play");
        //Invoke("_DEstroy",1);
    }
    void _DEstroy()
    {
        Destroy(gameObject);
    }
}
