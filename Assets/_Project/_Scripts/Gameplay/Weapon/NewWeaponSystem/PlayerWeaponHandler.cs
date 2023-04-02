using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WeaponSystem
{
    //Handling weapons by player
    public class PlayerWeaponHandler : MonoBehaviour
    {
        [SerializeField] private Weapon currentWeapon;
        [SerializeField] private Transform weaponHolder;
        
        public event Action<Weapon> OnSwitchingWeapon;
        public event Action<int> OnAmmoChange;

        private bool isWeaponReady = true;
        private bool isEquipped = false;
        
        private int currentIndex;

        private int _ammoPool = 100;

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
            ChangeWeapon(0, true);
        }

        private void Update()
        {
            HandleWeaponSwitching();
            
            isWeaponReady = currentWeapon != null && currentWeapon.isWeaponReady;
            
            if (isWeaponReady)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    currentWeapon.OnTrigger();
                }

                if (Input.GetMouseButtonUp(0))
                {
                    currentWeapon.OffTrigger();
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    currentWeapon.StartReload();
                }
            }
        }

        private void HandleWeaponSwitching()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeWeapon(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeWeapon(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeWeapon(2);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ChangeWeapon(-1);
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
        
        private void ChangeWeapon(int index, bool forceChange = false)
        {
            if (index == currentIndex && !forceChange) return;
            
            //Unequip previous weapon 
            if (currentWeapon != null)
            {
                currentWeapon.Unequip();
                currentWeapon = null;
            }

            if (index == -1)
            {
                isEquipped = false;
                OnSwitchingWeapon?.Invoke(null);
                foreach (Transform weapon in transform)
                {
                    weapon.gameObject.SetActive(false);
                }

                return;
            }
            
            int i = 0;
            foreach (Transform weapon in transform)
            {
                if (i == index)
                {
                    weapon.gameObject.SetActive(true);
                    currentWeapon = weapon.GetComponent<Weapon>();
                    currentWeapon.Equip(this);
                    OnSwitchingWeapon?.Invoke(currentWeapon);
                    currentIndex = i;
                    isEquipped = true;
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }
                i++;
            }
        }
    }
}
