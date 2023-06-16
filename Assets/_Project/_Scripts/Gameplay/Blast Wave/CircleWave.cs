 using System;
 using System.Collections;
using System.Collections.Generic;
 using DG.Tweening;
 using UnityEngine;

public class CircleWave : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
    }

    private void Start()
    {
        transform.DOScale(new Vector3(10, 0.1f, 10),1.25f);
    }
}
