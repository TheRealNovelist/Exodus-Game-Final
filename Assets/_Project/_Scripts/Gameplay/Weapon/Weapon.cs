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
        [SerializeField] private AudioManager audioManager;
        public Animator _animator;

        private AttackModule _primaryAttack;
        private AttackModule _secondaryAttack;
        
        private WeaponHandler _weaponHandler;
        
        [Space]
        [ReadOnly] public WeaponData data; //Internal data
        
        private IEnumerator _reloadRoutine => ReloadRoutine();
        private IEnumerator _equipRoutine => EquipRoutine();
        
        private int _currentAmmo;
        private bool _isReloading;
        public Transform owner;

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
            
           // _animator?.SetTrigger("Switch");
        }

        public void StartAttack()
        {
            if (!CanAttack())
            {
                audioManager.PlayOneShot("EmptyGunClicks");
                return; 
            }
            
            
            _animator?.SetTrigger("Shoot");
            try
            {
                _primaryAttack.StartAttack(this);
            }
            catch
            {
                Debug.Log("Primary Attack not found");
            }
        }

        public void StopAttack()
        {
            try
            {
                _primaryAttack.StopAttack();
            }
            catch
            {
                Debug.Log("Primary Attack not found");
            }
        }

        public void StopAllAttack()
        {
            try
            {
                _primaryAttack.StopAttack();
            }
            catch
            {
                Debug.Log("Primary Attack not found");
            }
        }
        
        private bool CanAttack()
        {
            return !_isReloading && _currentAmmo > 0 && IsWeaponReady;
        }

        public bool TryConsumeAmmo()
        {
            if (CurrentAmmo <= 0) return false;
            
            audioManager.PlayOneShot("GunShots");
            
            CurrentAmmo -= 1;
            return true;
        }

        public void StartReload()
        {
            if (CurrentAmmo == data.magazineSize || !_weaponHandler.CanReload(data)) return;
            audioManager.PlayOneShot("Reloads");
            _animator?.SetTrigger("Reload");
            
            StopAllAttack();
            
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
            audioManager.PlayOneShot("ChangeGunSound");
            _weaponHandler = weaponHandler;
            owner = weaponHandler.owner;
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
            
            // Cancel any weapon action
            StopAllAttack();
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
