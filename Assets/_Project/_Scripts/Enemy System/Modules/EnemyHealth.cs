using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float baseHealth;

 [HideInInspector]   public bool IsDamagable = true;
    
    public float Health { get; private set; }
    
    public event Action<float> OnDamaged;
    public event Action OnDied;

    private void Awake()
    {
        Health = baseHealth;
    }

    public void Damage(float amount, Transform source = null)
    {
        if (!IsDamagable) return;
        
        OnDamaged?.Invoke(amount);
        
        if (Health - amount < 0f)
        {
            Health = 0;
            OnDied?.Invoke();
            return;
        }
        
        Health -= amount;
    }

    public void ResetHealth()
    {
        Health = baseHealth;
    }
}
