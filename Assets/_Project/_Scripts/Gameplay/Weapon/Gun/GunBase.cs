using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBase : MonoBehaviour
{
    protected bool isReloading;
    
    [Header("Component")]
    [SerializeField] protected GunData gunData; // a reference to class Gundata:ScriptableObject
    [SerializeField] protected AmmoManager ammoManager; //reference to class AmmoManager

    protected float nextTimeToFire = 0f;
    
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    
    [Header("Key Binds")] 
    protected KeyCode shootKey = KeyCode.Mouse0;
    protected KeyCode reloadKey = KeyCode.R;
    
    protected IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Is reloading");
        gunData.ammoNeedToReload = Mathf.Min(gunData.maxMagazineAmmo - gunData.currentAmmoInsizeGunMagazine, ammoManager.ammoPlayerCurrentHave);
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmoInsizeGunMagazine += gunData.ammoNeedToReload;
        ammoManager.ammoPlayerCurrentHave -= gunData.ammoNeedToReload; 
        isReloading = false;
    }

    public abstract void Shoot();
}
