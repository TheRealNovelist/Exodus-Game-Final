using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class RaycastAttack : AttackModule
    {
        public Camera fpsCam;
        public float damage = 50;

        public override void Shoot()
        {
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out var hit))
            {
                Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * 1000, Color.red, 3f);

                if (hit.collider.gameObject.TryGetComponent(out IDamageable hitObject))
                {
                    hitObject.Damage(damage);
                }
            }
        }
    }
}
