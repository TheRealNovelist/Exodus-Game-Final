using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float baseHealth = 100f;
    [Range(0, 1f), SerializeField] private float baseDamageMultiplier = 1f;
    
    [HideInInspector] public bool isDamageable = true;

    [SerializeField] private MMF_Player feedback;
    
    public float Health { get; private set; }
    public float DamageMultiplier { get; private set; }
    
    public event Action<Transform> OnDamaged;
    public event Action OnDeath;

    private bool _isDead;
    
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
        
        if (_isDead) return;
        
        OnDamaged?.Invoke(source);
        feedback.PlayFeedbacks();
        
        amount *= baseDamageMultiplier;
        if (Health - amount <= 0f)
        {
            Health = 0;
            _isDead = true;
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
