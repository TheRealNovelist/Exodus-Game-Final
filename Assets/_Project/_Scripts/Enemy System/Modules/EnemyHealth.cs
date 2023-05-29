using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float baseHealth;

    public float Health { get; private set; }
    
    public event Action<float> OnDamaged;

    private void Awake()
    {
        Health = baseHealth;
    }

    public void Damage(float amount, Transform source = null)
    {
        OnDamaged?.Invoke(amount);
        
        if (Health - amount < 0f)
        {
            Health = 0;
            return;
        }
        
        Health -= amount;
    }
}
