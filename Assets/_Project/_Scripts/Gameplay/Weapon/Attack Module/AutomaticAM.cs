using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class AutomaticAM : AttackModule
    {
        [SerializeField] private AttackModule wrappedModule;
        
        private float _nextTimeToFire;
        
        public override void StartAttack(Weapon weapon, bool consumeAmmo = true)
        {

        }

        public override void HoldAttack(Weapon weapon, bool consumeAmmo = true)
        {
            if (!(Time.time >= _nextTimeToFire)) return;
            
            ConsumeAmmo(weapon, consumeAmmo);
            
            _nextTimeToFire = Time.time + 1f / weapon.data.fireRate;
            wrappedModule.StartAttack(weapon, false);
        }
    }
}
