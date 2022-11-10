using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;    //a delay before the grenade explode
    float countDown;    //timer until explosion
    public float blastRadius = 5f;  //the radius of the explosion
    public float explosionForce = 1000f;    //the power of the explosion
    public GameObject explosionEfex;    //explosion effects
    Rigidbody rb;
    public float throwForce = 200f;
    ThrowGrenade throwGrenade;
    //  GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);    //instantiate a grenade and store in a var
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   //get rigidbody from grenade
        countDown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        
        countDown -= Time.deltaTime;
        if (countDown <= 0)
        {
            Explosion();   
        }
    }

    void Explosion()
    {
        //Instantiate(explosionEfex, transform.position, transform.rotation); //instantiate effect of explosion

        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);  //stores colliders of nearby objects that the grenade is touching in an array

        foreach(Collider nearbyObject in colliders) //loop through each collider nearby
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();  //stores rb in a var
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);  //explode using AddExplosionForce()
            }
            
        }
        Debug.Log("BOOM!");
        Destroy(this.gameObject);   //Destroy grenade after explosion
    }
}
