using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WeaponSystem;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private PlayerWeaponHandler _weaponHandler;

    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private GameObject reloadText;

    private Weapon _currentWeapon;

    private void Awake()
    {
        reloadText.SetActive(false);
    }

    private void OnEnable()
    {
        _weaponHandler.OnSwitchingWeapon += UpdateSwitchingWeapon;   
    }
    
    private void OnDisable()
    {
        _weaponHandler.OnSwitchingWeapon -= UpdateSwitchingWeapon;
    }

    private void UpdateSwitchingWeapon(Weapon weapon)
    {
        if (_currentWeapon != null) DeregisterWeapon(_currentWeapon);
        RegisterWeapon(weapon);
        _currentWeapon = weapon;
    }

    private void RegisterWeapon(Weapon weapon)
    {
        weapon.OnAmmoChange += UpdateAmmoText;
        weapon.OnStartReload += UpdateReloadText;
    }
    
    private void DeregisterWeapon(Weapon weapon)
    {
        weapon.OnAmmoChange -= UpdateAmmoText;
        weapon.OnStartReload -= UpdateReloadText;
    }

    private void UpdateAmmoText(int ammo) => ammoText.text = "Ammo: " + ammo;

    private void UpdateReloadText(float duration) => StartCoroutine(ReloadRoutine(duration));
    
    private IEnumerator ReloadRoutine(float duration)
    {
        reloadText.SetActive(true);
        yield return new WaitForSeconds(duration);
        reloadText.SetActive(false);
    }
}
