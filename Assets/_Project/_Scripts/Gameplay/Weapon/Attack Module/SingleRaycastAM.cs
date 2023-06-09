using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace WeaponSystem
{
    public class SingleRaycastAM : AttackModule
    {
        public Camera fpsCam;
        public ParticleSystem muzzleFlash;
        [FormerlySerializedAs("impacEffect")] public GameObject impactEffect;

        public override void StartAttack(Weapon weapon, bool consumeAmmo = true)
        {
            //play partical effect
            muzzleFlash.Play();
            ConsumeAmmo(weapon, consumeAmmo);

            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out var hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out IDamageable hitObject))
                {
                    hitObject.Damage(weapon.data.damage);
                }
                else
                {
                    hitObject = hit.collider.gameObject.GetComponentInParent<IDamageable>();
                    
                    if (hitObject != null)
                        hitObject.Damage(weapon.data.damage);
                }

                Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
