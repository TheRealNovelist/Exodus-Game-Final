using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WeaponSystem
{
    public class SpreadRaycastAM : AttackModule
    {
        public Camera fpsCam;
        public ParticleSystem muzzleFlash;
        public GameObject impacEffect;


        public override void StartAttack(Weapon weapon, bool consumeAmmo = true)
        {

            //play partical effect 
            muzzleFlash.Play();

            ConsumeAmmo(weapon, consumeAmmo);


            List<GameObject> impactObjects = new List<GameObject>(); // List to store impact objects

            for (int i = 0; i < weapon.data.bulletPerShot; i++)
            {
                Vector3 direction = fpsCam.transform.forward; //initial aim
                Vector3 spread = Vector3.zero; //create a angle for us to put random angle in it to make random shot for each pellets
                spread += fpsCam.transform.up * Random.Range(-1f, 1f); // add random up and down
                spread += fpsCam.transform.right * Random.Range(-1f, 1f); // add random left and right

                //using random up and right value will lead to a square spray pattern if we normalize
                //this vector, we'll get the spread direction, but as a circle
                //change direction with the new spread angle 
                direction += spread.normalized * Random.Range(0, 0.2f);

                if (Physics.Raycast(fpsCam.transform.position, direction, out RaycastHit hit,AttackMask))
                {
                    if (hit.collider.gameObject.TryGetComponent(out IDamageable hitObject))
                    {
                        hitObject.Damage(weapon.data.damage);
                    }
                    // Instantiate impact effect at hit point
                    GameObject impactObj = Instantiate(impacEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    
                    impactObjects.Add(impactObj);
                    Debug.DrawLine(fpsCam.transform.position, hit.point, Color.green, 3f);
                }
                else
                {
                    Debug.DrawLine(
                        fpsCam.transform.position, fpsCam.transform.position + direction * weapon.data.range, 
                        Color.red, 3f);
                }
            }
        }
    }
}
