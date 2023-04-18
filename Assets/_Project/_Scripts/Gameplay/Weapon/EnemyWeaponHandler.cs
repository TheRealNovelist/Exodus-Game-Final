using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class EnemyWeaponHandler : WeaponHandler
    {
        public void Equip() => _currentWeapon.Equip(this);
        public void Unequip() => _currentWeapon.Unequip();
        public void Reload() => _currentWeapon.StartReload();
        public void CancelReload() => _currentWeapon.CancelReload();
        
        public override int TryAddAmmo(int currentAmmo, WeaponData data)
        {
            return data.magazineSize;
        }

        public override bool CanReload()
        {
            return true;
        }
    }
}
