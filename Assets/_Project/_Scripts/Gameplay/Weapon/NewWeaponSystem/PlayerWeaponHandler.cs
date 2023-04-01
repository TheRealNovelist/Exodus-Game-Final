using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WeaponSystem
{
    //Handling weapons by player
    public class PlayerWeaponHandler : MonoBehaviour
    {
        public Weapon currentWeapon;

        public event Action<Weapon> OnSwitchingWeapon;

        public void Start()
        {
            OnSwitchingWeapon?.Invoke(currentWeapon);
        }

        private void Update()
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
                currentWeapon.Reload();
            }
        }
    }
}
