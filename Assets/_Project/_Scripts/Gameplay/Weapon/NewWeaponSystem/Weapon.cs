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

        private int _currentAmmo;
        private int CurrentAmmo
        {
            get => _currentAmmo;
            set
            {
                _currentAmmo = value;
                OnAmmoChange?.Invoke(_currentAmmo);
            }
        }
        private bool isReloading;
        private float nextTimeToFire;
        private bool onTrigger;

        public event Action<float> OnStartReload;
        public event Action<int> OnAmmoChange;

        private void Start()
        {
            CurrentAmmo = data.MagazineSize;
        }

        private void Update()
        {
            if (Time.time >= nextTimeToFire && onTrigger)
            {
                nextTimeToFire = Time.time + 1f / data.FireRate;
                DealDamage();
            }    
        }

        public void OnTrigger()
        {
            switch (data.FiringMode)
            {
                case FiringMode.SemiAuto:
                    DealDamage();
                    break;
                case FiringMode.FullAuto:
                    onTrigger = true;
                    break;
            }
        }

        public void OffTrigger()
        {
            onTrigger = false;
        }
        
        private void DealDamage()
        {
            if (isReloading || _currentAmmo <= 0) return;
            CurrentAmmo -= 1;
            _attackModule.Attack(data.Damage);
        }
        
        public void Reload() => StartCoroutine(ReloadRoutine());
        
        private IEnumerator ReloadRoutine()
        {
            isReloading = true;
            OnStartReload?.Invoke(data.ReloadTime);
            
            yield return new WaitForSeconds(data.ReloadTime);
            CurrentAmmo = data.MagazineSize;
       
            isReloading = false;
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (dataSO != null)
            {
                data = dataSO.data;
            }


        }
#endif
    }
}
