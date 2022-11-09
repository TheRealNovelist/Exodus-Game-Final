using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun2_ShotGun : MonoBehaviour
{
    private bool isReloading;
    
    
    [Header("Component")]
    [SerializeField] private GunData gunData; // a reference to class Gundata:ScriptableObject
    [SerializeField] private AmmoManager ammoManager; //reference to class AmmoManager
     
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
        muzzleFlash.Play();
        
        //get position of the camera to shoot
        Transform spawnPoint = transform.Find("Player1/MainCamera");
        if (spawnPoint != null)
        {
            for (int i = 0; i < Mathf.Max(1, gunData.gunPellets); i++)
            {
                //bloom
                Vector3 t_bloom = spawnPoint.position + spawnPoint.forward * 1000f;
                //t_bloom += Random.Range()
            }
        }
        else
        {
            Debug.Log("Some thing wrong");
        }
        
        
        
        //decrease value of currentAmmo var
        gunData.currentAmmoInsizeGunMagazine--;
        
        
        
    }
}
