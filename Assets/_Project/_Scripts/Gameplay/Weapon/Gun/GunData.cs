using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunData : ScriptableObject
{
    public string gunName;
    public string gunType;
    public float gunDamage;
    public int gunPellets = 1; //number of bullets shot at one time
    public float gunRange;
    public float impactForce;
    public float fireRate;
    public int maxMagazineAmmo;
 //   public int ammoPlayerCurrentHave;
    public int currentAmmoInsizeGunMagazine;
    public int ammoNeedToReload;
    public float reloadTime;

}
