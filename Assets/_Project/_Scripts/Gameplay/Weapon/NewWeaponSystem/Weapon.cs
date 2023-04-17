using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace WeaponSystem
{
    public class Weapon : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private WeaponDataInjector dataInjector;
        [SerializeField] private AttackModule _attackModule;
        [SerializeField] private Modifier _modifier;

        [Space]
        public WeaponData data; //Internal data
        
        private IEnumerator _reloadRoutine => ReloadRoutine();
        private IEnumerator _equipRoutine => EquipRoutine();
        
        private int _currentAmmo;
        public int CurrentAmmo
        {
            get => _currentAmmo;
            private set
            {
                _currentAmmo = value;
                OnAmmoChange?.Invoke(_currentAmmo);
            }
        }
        
        private bool _isReloading;
        private float _nextTimeToFire;
        private bool _onTrigger;

        private WeaponHandler _weaponHandler;
        
        public bool IsWeaponReady { get; private set; }

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
        }

        private void Update()
        {
            if (Time.time >= _nextTimeToFire && _onTrigger)
            {
                _nextTimeToFire = Time.time + 1f / data.fireRate;
                DealDamage();
            }    
        }

        public void OnTrigger()
        {
            switch (data.firingMode)
            {
                case FiringMode.Single:
                    DealDamage();
                    break;
                case FiringMode.Hold:
                    _onTrigger = true;
                    break;
            }
        }

        public void OffTrigger()
        {
            _onTrigger = false;
            _attackModule.EndAttack();
        }
        
        private void DealDamage()
        {
            if (_isReloading || _currentAmmo <= 0) return;
            CurrentAmmo -= 1;
            _attackModule.Attack(data);
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
            // Cancel trigger
            OffTrigger();

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
