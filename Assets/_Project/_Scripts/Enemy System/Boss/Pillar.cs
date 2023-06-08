using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Pillar : MonoBehaviour, IDamageable
{
    [SerializeField] private float baseHealth = 100f;
    [SerializeField] private BossProjectileShooter shooter;
    [SerializeField] private float fireRate;

    private Animator _animator => GetComponent<Animator>();
    private TargetLocator _targetLocator => GetComponentInParent<TargetLocator>();
    
    private float _currentHealth;
    private bool _isDamageable;
    private float _nextTimeToFire;
    
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
            DisablePillar();
        }
    }

    private void Update()
    {
        shooter.transform.LookAt(_targetLocator.Target.transform);
        
        if (!_isDamageable) return;
        
        if (!(Time.time >= _nextTimeToFire)) return;
        _nextTimeToFire = Time.time + 1f / fireRate;
        shooter.Attack();
    }

    public void StartDamageable()
    {
        _isDamageable = true;
    }

    public void DisablePillar()
    {
        gameObject.SetActive(false);
    }
    
    public void StartDisablePillar()
    {
        _isDamageable = false;
        _animator.SetTrigger("Disable");
    }
}
