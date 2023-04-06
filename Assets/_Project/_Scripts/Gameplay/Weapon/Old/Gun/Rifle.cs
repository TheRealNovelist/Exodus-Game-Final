using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Old
{
    public class Rifle : GunBase
    {
        private void Start()
        {
            gunData.currentAmmo = gunData.maxMagazineAmmo; //let player start with a gun ful with ammo
            //  MakeCameraRecoil_Script = transform.Find("CamRot/CameraRecoil").GetComponent<MakeCameraRecoil>();
        }

        void Update()
        {
            // Debug.Log("I have " + ammoManager.ammoPlayerCurrentHave + " bullets, I have " + gunData.currentAmmoInsizeGunMagazine + " bullets inside my gun" );

            if (isReloading)
                return;

            if (Input.GetKey(reloadKey))
            {
                StartCoroutine(Reload());
                return;
            }

            if (Input.GetKey(shootKey) && Time.time >= nextTimeToFire && gunData.currentAmmo > 0)
            {
                nextTimeToFire = Time.time + 1f / gunData.fireRate;
                Shoot();
            }
        }


        public override void Shoot()
        {
            //MakeCameraRecoil_Script.RecoilFire();
            muzzleFlash.Play();

            //decrease value of currentAmmo var
            gunData.currentAmmo--;

            RaycastHit hit; // var to save data about what we hit 
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, gunData.gunRange))
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
        }
    }
}
