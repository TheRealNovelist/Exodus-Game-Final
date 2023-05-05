using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace WeaponSystem
{
    public enum WeaponMode
    {
        Primary,
        Secondary
    }
    
    public class Weapon : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private WeaponDataInjector dataInjector;
        [SerializeField] private AttackModule defaultAttackModule;

        private AttackModule _primaryAttack;
        private AttackModule _secondaryAttack;
        
        private WeaponHandler _weaponHandler;
        
        [Space]
        public WeaponData data; //Internal data
        
        private IEnumerator _reloadRoutine => ReloadRoutine();
        private IEnumerator _equipRoutine => EquipRoutine();
        
        private int _currentAmmo;
        private bool _isReloading;
        
        #region Properties
        public int CurrentAmmo
        {
            get => _currentAmmo;
            private set
            {
                _currentAmmo = value;
                OnAmmoChange?.Invoke(_currentAmmo);
            }
        }
        
        public bool IsWeaponReady { get; private set; }

        #endregion

        #region Events
        
        public event Action OnStartReload;
        public event Action OnEndReload;
        
        public event Action OnStartEquip;
        public event Action OnFinishEquip;
        public event Action OnUnequip;
        
        public event Action<int> OnAmmoChange;

        #endregion

        private void Start()
        {
            data = dataInjector.TryGetData();
            
            CurrentAmmo = data.magazineSize;

            _primaryAttack = defaultAttackModule;
        }

        public void StartAttack(WeaponMode mode)
        {
            if (!CanAttack()) return;
            switch (mode)
            {
                case WeaponMode.Primary:
                    try
                    {
                        _primaryAttack.StartAttack(this);
                    }
                    catch
                    {
                        Debug.Log("Primary Attack not found");
                    }
                    break;
                case WeaponMode.Secondary:
                    try
                    {
                        _secondaryAttack.StartAttack(this);
                    }
                    catch
                    {
                        Debug.Log("Secondary Attack not found");
                    }
                    break;
            }
        }

        public void HoldAttack(WeaponMode mode)
        {
            if (!CanAttack()) return;
            switch (mode)
            {
                case WeaponMode.Primary:
                    try
                    {
                        _primaryAttack.HoldAttack(this);
                    }
                    catch
                    {
                        Debug.Log("Primary Attack not found");
                    }
                    break;
                case WeaponMode.Secondary:
                    try
                    {
                        _secondaryAttack.HoldAttack(this);
                    }
                    catch
                    {
                        Debug.Log("Secondary Attack not found");
                    }
                    break;
            }
        }
        
        private bool CanAttack()
        {
            return !_isReloading && _currentAmmo > 0;
        }

        public void ConsumeAmmo()
        {
            CurrentAmmo -= 1;
        }

        public void StartReload()
        {
            if (CurrentAmmo == data.magazineSize || !_weaponHandler.CanReload()) return;
            
            StartCoroutine(_reloadRoutine);
        }

        public void CancelReload()
        {
            if (!_isReloading) return;
            
            StopCoroutine(_reloadRoutine);
            
            _isReloading = false;
            OnEndReload?.Invoke();
        } 
        
        private IEnumerator ReloadRoutine()
        {
            _isReloading = true;
            var reloadTime = _currentAmmo > 0 ? data.fastReloadTime : data.normalReloadTime;
            OnStartReload?.Invoke();
            
            yield return new WaitForSeconds(reloadTime);
            
            _isReloading = false;
            CurrentAmmo = _weaponHandler.TryAddAmmo(CurrentAmmo, data);
            OnEndReload?.Invoke();
        }

        public void Equip(WeaponHandler weaponHandler)
        {
            _weaponHandler = weaponHandler;
            StartCoroutine(_equipRoutine);
        }

        private IEnumerator EquipRoutine()
        {
            IsWeaponReady = false;
            OnStartEquip?.Invoke();

            yield return new WaitForSeconds(data.equipTime);
            
            IsWeaponReady = true;
            OnFinishEquip?.Invoke();
        }
        
        public void Unequip()
        {
            StopCoroutine(_equipRoutine);

            _weaponHandler = null;
            
            OnUnequip?.Invoke();
            
            // Cancel reload if needed
            CancelReload();

            IsWeaponReady = false;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            //data = dataSO != null ? dataSO.GetData() : new WeaponData();
        }
#endif
    }
}
