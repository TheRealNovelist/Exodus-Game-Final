using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class RaycastAttackModule : AttackModule
    {
        public Camera fpsCam;

        public override void Attack(float damage)
        {
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out var hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out IDamageable hitObject))
                {
                    hitObject.Damage(damage);
                }
            }
        }
    }
}
