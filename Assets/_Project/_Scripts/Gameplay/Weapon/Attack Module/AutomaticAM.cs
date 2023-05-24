using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class AutomaticAM : AttackModule
    {
        [SerializeField] private AttackModule wrappedModule;
        
        private float _nextTimeToFire;
        private bool _isAttacking;
        private bool _consumeAmmo;

        private Weapon _weapon;
        
        public override void StartAttack(Weapon weapon, bool consumeAmmo = true)
        {
            _weapon = weapon;
            _consumeAmmo = consumeAmmo;

            _isAttacking = true;
        }

        private void Update()
        {
            if (!_isAttacking) return;
            if (!(Time.time >= _nextTimeToFire)) return;
            
            ConsumeAmmo(_weapon, _consumeAmmo);
            
            _nextTimeToFire = Time.time + 1f / _weapon.data.fireRate;
            wrappedModule.StartAttack(_weapon, false);
        }
        
        public override void StopAttack()
        {
            _isAttacking = false;
        }
    }
}
