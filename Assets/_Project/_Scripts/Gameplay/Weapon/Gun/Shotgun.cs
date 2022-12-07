using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shotgun : GunBase
{
    private void Start()
    {
        gunData.currentAmmoInsizeGunMagazine = gunData.maxMagazineAmmo; //let player start with a gun ful with ammo
        //  MakeCameraRecoil_Script = transform.Find("CamRot/CameraRecoil").GetComponent<MakeCameraRecoil>();
    }

    void Update()
    {
        Debug.Log("I have " + ammoManager.ammoPlayerCurrentHave + " bullets, I have " + gunData.currentAmmoInsizeGunMagazine + " bullets inside my gun" );

        if(isReloading)
            return;
        
        if (Input.GetKey(reloadKey))
        {
           StartCoroutine(Reload()) ;
           return;
        } 
        if (Input.GetKey(shootKey) && Time.time >= nextTimeToFire && gunData.currentAmmoInsizeGunMagazine > 0 )
        {
            nextTimeToFire = Time.time + 1f / gunData.fireRate;
            Shoot();
        }    
    }

    public override void Shoot()
    {
        muzzleFlash.Play(); //player muzzleFlash
        
        //create numbers of of projectiles equal to the number of pellets 
        for (int i = 0; i < gunData.gunPellets; i++)
        {
            Vector3 direction = fpsCam.transform.forward; //initial aim
            Vector3 spread = Vector3.zero; //create a angle for us to put random angle in it to make random shot for each pellets
            spread += fpsCam.transform.up * Random.Range(-1f, 1f); // add random up and down
            spread += fpsCam.transform.right * Random.Range(-1f, 1f); // add random left and right
            
            //using random up and right value will lead to a square spray pattern if we normalize
            //this vector, we'll get the spread direction, but as a circle
            //change direction with the new spread angle 
            direction += spread.normalized * Random.Range(0, 0.2f);
            
            RaycastHit hit; // var to save data about what we hit 
            if (Physics.Raycast(fpsCam.transform.position, direction, out hit, gunData.gunRange))
            {
                IDamageable damagableObject = hit.transform.GetComponent<IDamageable>();
            
                if (damagableObject != null)
                {
                    damagableObject.Damage(gunData.gunDamage);
                }

                if (hit.rigidbody != null) 
                {
                    hit.rigidbody.AddForce(-hit.normal * gunData.impactForce);
                }
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 5.0f);
            }
            if (Physics.Raycast(fpsCam.transform.position, direction, out hit, gunData.gunRange))
            {
                Debug.DrawLine(fpsCam.transform.position, hit.point, Color.green, 3f);
            }
            else
            {
                Debug.DrawLine(fpsCam.transform.position, fpsCam.transform.position + direction * gunData.gunRange, Color.red, 3f);
            }
        }
        
        
        //decrease value of currentAmmo var
        gunData.currentAmmoInsizeGunMagazine--;
    }
}
