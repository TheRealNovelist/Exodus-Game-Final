using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public abstract class WeaponHandler : MonoBehaviour
    {
        [SerializeField] protected Weapon _currentWeapon;
        
        public abstract int TryAddAmmo(int currentAmmo, WeaponData data);

        public abstract bool CanReload();
    }
}
