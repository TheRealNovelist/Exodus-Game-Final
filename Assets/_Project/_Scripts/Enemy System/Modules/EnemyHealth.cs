using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float baseHealth = 100f;
    [Range(0, 1f), SerializeField] private float baseDamageMultiplier = 1f;
    
    [HideInInspector] public bool isDamageable = true;
    
    public float Health { get; private set; }
    public float DamageMultiplier { get; private set; }
    
    public event Action<Transform> OnDamaged;
    public event Action OnDeath;

    private void Awake()
    {
        Health = baseHealth;
        DamageMultiplier = baseDamageMultiplier;
    }

    public void SetDamageMultiplier(float percentage = -1f)
    {
        if (percentage <= 0)
        {
            DamageMultiplier = baseDamageMultiplier;
            return;
        }
        
        DamageMultiplier = percentage;
    }
    
    public void Damage(float amount, Transform source = null)
    {
        if (!isDamageable) return;
        
        OnDamaged?.Invoke(source);
        
        amount *= baseDamageMultiplier;
        if (Health - amount < 0f)
        {
            Health = 0;
            OnDeath?.Invoke();
            return;
        }
        
        Health -= amount;
    }

    public void ResetHealth()
    {
        Health = baseHealth;
    }
}
