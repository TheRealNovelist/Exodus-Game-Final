using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WeaponSystem
{
    //Handling weapons by player
    public class WeaponHandler : MonoBehaviour
    {
        public Transform owner;
        [SerializeField] private Weapon currentWeapon;
        [SerializeField] private Transform weaponHolder;
        [SerializeField] private int maxAmmo = 100;
        
        public event Action<Weapon> OnSwitchingWeapon;
        public event Action<int> OnAmmoChange;

        private bool isWeaponReady => currentWeapon != null && currentWeapon.IsWeaponReady;
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
           // ChangeWeapon(0, true);
            if (Inventory.Instance != null) Inventory.Instance.OnGunEquipped += ReorderGunChildren;
        }
        
        public void Awake()
        {
            var input = PlayerInputManager.Input;

            input.Weapon.PrimaryAttack.performed += 
                (ctx) => StartAttack();
            input.Weapon.PrimaryAttack.canceled += 
                (ctx) => StopAttack();

            input.Weapon.Reload.performed += 
                (ctx) => Reload();
            
            input.General.ChangeWeapon.performed += 
                (ctx) => ScrollChangeWeapon();
            
            input.General.Weapon1.performed += 
                (ctx) => ChangeWeapon(0);
            
            input.General.Weapon2.performed +=
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
        

        private void StartAttack()
        {
            if (!isWeaponReady) return;
            currentWeapon.StartAttack();
        }

        private void StopAttack()
        {
            if (!isWeaponReady) return;
            currentWeapon.StopAttack();
        }

        private void Reload()
        {
            if (!isWeaponReady) return;
            currentWeapon.StartReload();
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
            
            //If ammoCost does not consume ammo, return ammo needed to reload
            if (data.ammoCostPerBullet == 0) return data.magazineSize;
            
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

        public WeaponData CurrentWeaponData => currentWeapon.data;

        public bool CanReload(WeaponData data)
        {
            return _ammoPool > 0 || data.ammoCostPerBullet == 0;
        }

        private void ChangeWeapon(int index, bool forceChange = false)
        {
            if (index == currentIndex && !forceChange) return;
            if (index > Inventory.Instance.EquippedGunsQuantity()-1)  return;

            //Unequip previous weapon 
            if (currentWeapon != null)
            {
                currentWeapon.Unequip();
                currentWeapon._animator?.SetTrigger("Unequip");
                currentWeapon = null;
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
                    currentWeapon = weapon.GetComponent<Weapon>();
                    currentWeapon.Equip(this);
                    currentWeapon._animator?.SetTrigger("Equip");
                    OnSwitchingWeapon?.Invoke(currentWeapon);
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
