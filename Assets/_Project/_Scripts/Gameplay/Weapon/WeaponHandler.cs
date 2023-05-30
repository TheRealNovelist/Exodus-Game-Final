using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WeaponSystem
{
    //Handling weapons by player
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private Weapon _currentWeapon;
        [SerializeField] private Transform weaponHolder;
        [SerializeField] private int maxAmmo = 100;
        
        public event Action<Weapon> OnSwitchingWeapon;
        public event Action<int> OnAmmoChange;

        private bool isWeaponReady => _currentWeapon != null && _currentWeapon.IsWeaponReady;
        private int currentIndex;

        private int _ammoPool;

        public int AmmoPool
        {
            get => _ammoPool;
            private set
            {
                _ammoPool = value;
                OnAmmoChange?.Invoke(_ammoPool);
            }
        }

        public void Start()
        {
            AmmoPool = maxAmmo;
            ChangeWeapon(0, true);
            //if (Inventory.Instance != null) Inventory.Instance.OnGunEquipped += ReorderGunChildren;
        }
        
        public void Init()
        {
            PlayerInputManager.Input.Weapon.PrimaryAttack.performed += 
                (ctx) => StartAttack(WeaponMode.Primary);
            PlayerInputManager.Input.Weapon.PrimaryAttack.canceled += 
                (ctx) => StopAttack(WeaponMode.Primary);
            
            PlayerInputManager.Input.Weapon.SecondaryAttack.performed += 
                (ctx) => StartAttack(WeaponMode.Secondary);
            PlayerInputManager.Input.Weapon.SecondaryAttack.canceled += 
                (ctx) => StopAttack(WeaponMode.Secondary);
            
            PlayerInputManager.Input.Weapon.Reload.performed += 
                (ctx) => Reload();
            
            PlayerInputManager.Input.General.ChangeWeapon.performed += 
                (ctx) => ScrollChangeWeapon();
            
            PlayerInputManager.Input.General.Weapon1.performed += 
                (ctx) => ChangeWeapon(0);
            
            PlayerInputManager.Input.General.Weapon2.performed +=
                (ctx) => ChangeWeapon(1);
        }

        private void ReorderGunChildren(WeaponDataSO data, int index)
        {
            foreach (SOInjector injector in transform.GetComponentsInChildren<SOInjector>(true))
            {
                if (injector.DataSO == data)
                {
                    injector.gameObject.transform.SetSiblingIndex(index);
                }

                if (index == 0)
                {
                    currentIndex = 1;
                    ChangeWeapon(0, true);
                }
            }
        }
        

        private void StartAttack(WeaponMode mode)
        {
            if (!isWeaponReady) return;
            _currentWeapon.StartAttack(mode);
        }

        private void StopAttack(WeaponMode mode)
        {
            if (!isWeaponReady) return;
            _currentWeapon.StopAttack(mode);
        }

        private void Reload()
        {
            if (!isWeaponReady) return;
            _currentWeapon.StartReload();
        }
        
        private void ScrollChangeWeapon()
        {
            switch (currentIndex)
            {
                case 0:
                    ChangeWeapon(1);
                    break;
                case 1:
                    ChangeWeapon(0);
                    break;
            }
        }

        public int TryAddAmmo(int currentAmmo, WeaponData data)
        {
            int ammo = currentAmmo;

            int ammoNeededToReload = data.magazineSize - ammo;
            int ammoCost = ammoNeededToReload * data.ammoCostPerBullet;

            if (ammoCost > AmmoPool)
            {
                //Add in any possible ammo conversion
                ammo += AmmoPool / data.ammoCostPerBullet;
                //Ammo pool get remaining amount after conversion
                AmmoPool %= data.ammoCostPerBullet;
            }
            else
            {
                ammo += ammoNeededToReload;
                AmmoPool -= ammoCost;
            }

            return ammo;
        }

        public bool CanReload()
        {
            return _ammoPool > 0;
        }

        private void ChangeWeapon(int index, bool forceChange = false)
        {
            if (index == currentIndex && !forceChange) return;
            //if (index > Inventory.Instance.EquippedGunsQuantity()-1)  return;

            //Unequip previous weapon 
            if (_currentWeapon != null)
            {
                _currentWeapon.Unequip();
                _currentWeapon = null;
            }

            if (index == -1)
            {
                OnSwitchingWeapon?.Invoke(null);
                foreach (Transform weapon in weaponHolder)
                {
                    weapon.gameObject.SetActive(false);
                }

                return;
            }

            int i = 0;
            foreach (Transform weapon in weaponHolder)
            {
                if (i == index)
                {
                    weapon.gameObject.SetActive(true);
                    _currentWeapon = weapon.GetComponent<Weapon>();
                    _currentWeapon.Equip(this);
                    OnSwitchingWeapon?.Invoke(_currentWeapon);
                    currentIndex = i;
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }

                i++;
            }
        }

        public void AddAmmoOnPool()
        {
            if (AmmoPool == maxAmmo)
                return;

            AmmoPool += 100;
            if (AmmoPool >= maxAmmo)
            {
                AmmoPool = maxAmmo;
            }
        }
    }
}
