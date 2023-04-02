using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WeaponSystem;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private PlayerWeaponHandler _weaponHandler;

    [SerializeField] private GameObject gunCanvas;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI ammoPoolText;
    [SerializeField] private GameObject reloadText;

    private Weapon _currentWeapon;

    private void Awake()
    {
        reloadText.SetActive(false);
    }

    private void OnEnable()
    {
        _weaponHandler.OnSwitchingWeapon += UpdateSwitchingWeapon;
        _weaponHandler.OnAmmoChange += UpdateAmmoPoolText;
        UpdateAmmoPoolText(_weaponHandler.AmmoPool);
    }
    
    private void OnDisable()
    {
        _weaponHandler.OnSwitchingWeapon -= UpdateSwitchingWeapon;
        _weaponHandler.OnAmmoChange -= UpdateAmmoPoolText;
    }

    private void UpdateSwitchingWeapon(Weapon weapon)
    {
        if (_currentWeapon != null) DeregisterWeapon(_currentWeapon);

        if (weapon == null)
        {
            gunCanvas.SetActive(false);
        }
        else
        {
            gunCanvas.SetActive(true);
            RegisterWeapon(weapon);
            _currentWeapon = weapon;
        }
    }

    private void RegisterWeapon(Weapon weapon)
    {
        weapon.OnAmmoChange += UpdateAmmoText;
        UpdateAmmoText(weapon.CurrentAmmo);
        
        weapon.OnStartReload += UpdateStartReloadText;
        weapon.OnEndReload += UpdateEndReloadText;
    }
    
    private void DeregisterWeapon(Weapon weapon)
    {
        weapon.OnAmmoChange -= UpdateAmmoText;
        weapon.OnStartReload -= UpdateStartReloadText;
        weapon.OnEndReload -= UpdateEndReloadText;
    }

    private void UpdateAmmoText(int ammo) => ammoText.text = "Ammo: " + ammo;
    
    private void UpdateAmmoPoolText(int ammo) => ammoPoolText.text = "Pool: " + ammo;

    private void UpdateStartReloadText()
    {
        reloadText.SetActive(true);
    }

    private void UpdateEndReloadText()
    {
        reloadText.SetActive(false);
    }
}
