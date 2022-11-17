using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;

    [SerializeField] private GunData gunData;
    
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectWeapon = selectedWeapon;
        
        //Change gun by wheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon = 1;
            }
            //Add animation

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0f)
            {
                selectedWeapon = 1;
            }
            else
            {
                selectedWeapon = 0;
            }
            //Add animation
        }

        //Change gun by key
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Add animation
            
            selectedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            //Add animation

            selectedWeapon = 1;
        }

        if (previousSelectWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }
}
