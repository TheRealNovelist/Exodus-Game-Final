using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public UnityEvent OnStartAttack;
    public UnityEvent OnAttacking;
    public UnityEvent OnStopAttack;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }
}
