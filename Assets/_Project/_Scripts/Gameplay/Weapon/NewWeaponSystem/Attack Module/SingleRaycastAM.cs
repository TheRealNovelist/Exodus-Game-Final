using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class SingleRaycastAM : AttackModule
    {
        public Camera fpsCam;

        public override void Attack(WeaponData data)
        {
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out var hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out IDamageable hitObject))
                {
                    hitObject.Damage(data.damage);
                }
            }
        }
    }
}
