using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WeaponSystem
{
    //Handling weapons by player
    public class PlayerWeaponHandler : WeaponHandler
    {
        [SerializeField] private Transform weaponHolder;
        public event Action<Weapon> OnSwitchingWeapon;
        public event Action<int> OnAmmoChange;

        private bool isWeaponReady = true;
        private bool isEquipped = false;

        private int currentIndex;


        [SerializeField] private int maxAmmo = 100;

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

        }

        private void Awake()
        {
            Inventory.Instance.OnGunEquiped += ReorderGunChildren;

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
                    ChangeWeapon(0,true);
                }
            }
        }

        private void Update()
        {
            HandleWeaponSwitching();

            isWeaponReady = _currentWeapon != null && _currentWeapon.IsWeaponReady;

            if (!isWeaponReady) return;

            if (Input.GetMouseButtonDown(0))
            {
                _currentWeapon.StartAttack(WeaponMode.Primary);
            }

            if (Input.GetMouseButton(0))
            {
                _currentWeapon.HoldAttack(WeaponMode.Primary);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                _currentWeapon.StartReload();
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

            /*if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeWeapon(2);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ChangeWeapon(-1);
            }*/
        }

        public override int TryAddAmmo(int currentAmmo, WeaponData data)
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

        public override bool CanReload()
        {
            return _ammoPool > 0;
        }

        private void ChangeWeapon(int index, bool forceChange = false)
        {
            if (index == currentIndex && !forceChange) return;

            //Unequip previous weapon 
            if (_currentWeapon != null)
            {
                _currentWeapon.Unequip();
                _currentWeapon = null;
            }

            if (index == -1)
            {
                isEquipped = false;
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
                    isEquipped = true;
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