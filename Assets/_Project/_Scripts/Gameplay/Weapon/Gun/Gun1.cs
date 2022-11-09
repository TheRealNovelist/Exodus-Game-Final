using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class Gun1 : MonoBehaviour
{
    //Gonna put some where else later, this is still basic code
    
    private bool isReloading;
    
    
    [Header("Component")]
    [SerializeField] private GunData gunData; // a reference to class Gundata:ScriptableObject
    [SerializeField] private AmmoManager ammoManager; //reference to class AmmoManager
    [SerializeField] private MakeCameraRecoil MakeCameraRecoil_Script; // a reference to class MakeCameraRecoil
    
    private float nextTimeToFire = 0f;
    
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    
    [Header("Key Binds")] 
    public KeyCode shootKey = KeyCode.Mouse0;
    public KeyCode reloadKey = KeyCode.R;

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

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Is reloading");
        gunData.ammoNeedToReload = Mathf.Min(gunData.maxMagazineAmmo - gunData.currentAmmoInsizeGunMagazine, ammoManager.ammoPlayerCurrentHave);
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmoInsizeGunMagazine += gunData.ammoNeedToReload;
        ammoManager.ammoPlayerCurrentHave -= gunData.ammoNeedToReload; 
        isReloading = false;
      
    }
    void Shoot()
    {
        MakeCameraRecoil_Script.RecoilFire();
        muzzleFlash.Play();
        
        //decrease value of currentAmmo var
        gunData.currentAmmoInsizeGunMagazine--;
        
        RaycastHit hit; // var to save data about what we hit 
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, gunData.gunRange))
        {
            IDamageable damagableObject = hit.transform.GetComponent<IDamageable>();
            
            if (damagableObject != null)
            {
                damagableObject.TakeDamage(gunData.gunDamage);
            }

            if (hit.rigidbody != null) 
            {
                hit.rigidbody.AddForce(-hit.normal * gunData.impactForce);
            }
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 5.0f);
        }
    }

    
}
