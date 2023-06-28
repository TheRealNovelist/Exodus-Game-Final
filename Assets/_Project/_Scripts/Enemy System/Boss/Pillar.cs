using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Pillar : Enemy
{
    [SerializeField] private Transform pillarRootRotate, baseTower;
    [SerializeField] private BossProjectileShooter shooter;
    [SerializeField] private float fireRate;
    [SerializeField] private float turnSpeed;
    private Transform shooterTransform => shooter.transform;

    private Transform _target;

    private float _nextTimeToFire;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    protected override void OnEnable()
    {
        _enemyHealth.isDamageable = false;

        baseTower.transform.position = new Vector3(baseTower.transform.position.x, -17, baseTower.transform.position.z);
        baseTower.transform.DOMoveY(0, 7, false).OnComplete(() => { _enemyHealth.isDamageable = true; });

        _enemyHealth.ResetHealth();
        
        _enemyHealth.OnDeath += DisablePillar;
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        _enemyHealth.OnDeath -= DisablePillar;
        base.OnDisable();
    }

    private void Update()
    {
        UpdateTurretRotation();

        if (!_enemyHealth.isDamageable) return;

        if (!(Time.time >= _nextTimeToFire)) return;
        _nextTimeToFire = Time.time + 1f / fireRate;
        shooter.Attack();
    }

    public void UpdateTurretRotation()
    {
        pillarRootRotate.RotateTowards(_target, Time.deltaTime * turnSpeed, freezeX: true, freezeZ: true);
        shooterTransform.RotateTowards(_target, freezeY: true, freezeZ: true);
    }

    public void DisablePillar()
    {
        gameObject.SetActive(false);
    }

    public void StartDisablePillar()
    {
        _enemyHealth.isDamageable = false;
        baseTower.transform.DOMoveY(-17, 7, false).OnComplete(DisablePillar);
    }
}