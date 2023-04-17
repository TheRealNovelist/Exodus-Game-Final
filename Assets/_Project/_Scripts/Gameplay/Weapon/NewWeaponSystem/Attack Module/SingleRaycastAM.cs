using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class SingleRaycastAM : AttackModule
    {
        public Camera fpsCam;

        public override void StartAttack(Weapon weapon, bool consumeAmmo = true)
        {
            ConsumeAmmo(weapon, consumeAmmo);

            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out var hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out IDamageable hitObject))
                {
                    hitObject.Damage(weapon.data.damage);
                }
            }
        }
        
        public override void HoldAttack(Weapon weapon, bool consumeAmmo = true)
        {

        }
    }
}
