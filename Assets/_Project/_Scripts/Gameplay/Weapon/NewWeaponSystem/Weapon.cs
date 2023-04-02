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
        [SerializeField] private WeaponDataSO dataSO;
        [ReadOnly] public WeaponData data;

        [SerializeField] private AttackModule _attackModule;
        [SerializeField] private Renderer _debug;

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

        private PlayerWeaponHandler _weaponHandler;

        private Color _debugColor;
        
        public bool isWeaponReady { get; private set; }

        public event Action OnStartReload;
        public event Action OnEndReload;
        public event Action<int> OnAmmoChange;

        private void Awake()
        {
            _debugColor = _debug.material.color;
        }

        private void Start()
        {
            data = dataSO.GetData();
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
                case FiringMode.SemiAuto:
                    DealDamage();
                    break;
                case FiringMode.FullAuto:
                    _onTrigger = true;
                    break;
            }
        }

        public void OffTrigger()
        {
            _onTrigger = false;
        }
        
        private void DealDamage()
        {
            if (_isReloading || _currentAmmo <= 0) return;
            CurrentAmmo -= 1;
            _attackModule.Attack(data);
        }

        public void StartReload()
        {
            if (CurrentAmmo == data.magazineSize || _weaponHandler.AmmoPool <= 0) return;
            
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

        public void Equip(PlayerWeaponHandler weaponHandler)
        {
            _weaponHandler = weaponHandler;
            StartCoroutine(_equipRoutine);
        }

        public IEnumerator EquipRoutine()
        {
            isWeaponReady = false;
            
            _debug.material.color = Color.red;

            yield return new WaitForSeconds(data.equipTime);

            _debug.material.color = _debugColor;
            isWeaponReady = true;
        }
        
        public void Unequip()
        {
            StopCoroutine(_equipRoutine);
            
            // Cancel reload if needed
            CancelReload();
            // Cancel trigger
            OffTrigger();
            
            _debug.material.color = _debugColor;
            
            isWeaponReady = false;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            data = dataSO != null ? dataSO.GetData() : new WeaponData();
        }
#endif
    }
}
