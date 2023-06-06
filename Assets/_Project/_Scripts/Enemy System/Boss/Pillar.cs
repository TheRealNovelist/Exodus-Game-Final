using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour, IDamageable
{
    [SerializeField] private float baseHealth = 100f;

    private float _currentHealth;
    
    private bool _isDamageable;
    
    public void OnEnable()
    {
        _currentHealth = baseHealth;
    }

    public void Damage(float amount, Transform source = null)
    {
        if (!_isDamageable) return;

        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void StartDamageable()
    {
        _isDamageable = true;
    }
    
    private void OnDisable()
    {
        
    }
}
