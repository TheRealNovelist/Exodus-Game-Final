
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GunPickUpController : MonoBehaviour
{
    public Rifle gunScript; // reference to Gun1 class
    public Shotgun gunScript2;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;
    public float pickUpRange;

    public float dropForwardForce, dropUpwardForce;
    
    public bool equipped;
    public static bool slotFull;

    [Header("Key binds")]
    public KeyCode pickUpGun = KeyCode.E;
    public KeyCode putDownGun = KeyCode.Q;
    public void Start()
    {
        //Setup
        if (!equipped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }

        if (equipped)
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update()
    {
        //check if player is in range and pickUpGun is press
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(pickUpGun) && !slotFull)
        {
            PickUp();
        }
        //check if player want to drop gun
        if (equipped && Input.GetKeyDown(putDownGun))
        {
            Drop();
        }
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;
        //Make weapon a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
        
        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;
        
        //Enable script
        gunScript.enabled = true;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;
        
        //Set parent to null
        transform.SetParent(null);
        
        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = false;
        coll.isTrigger = false;
        
        //Make gun carry momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;
        
        //Add force
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10f);
        //Enable script
        gunScript.enabled = false;
    }
}
