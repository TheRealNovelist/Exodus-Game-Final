using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualPickUpForGun : MonoBehaviour
{
    private GunPickUp2 _gunPickUp2;
    // Start is called before the first frame update
    void Start()
    {
        _gunPickUp2 = GameObject.Find("GunHolder").GetComponent<GunPickUp2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //add this gun to 
        }
    }
}
