using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    public float throwForce = 50f;
    public GameObject grenadePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //call function to throw grenade
        if (Input.GetKeyDown(KeyCode.G))
        {
            GrenadeThrow();
        } 
        
    }
    void GrenadeThrow()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);    //instantiate a grenade and store in a var
        Rigidbody rb = grenade.GetComponent<Rigidbody>();   //get rigidbody from grenade
        rb.AddForce(transform.forward * throwForce);    //add force to throw grenade
        Debug.Log("Throwing grenade");
    }
}
