using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BossBeamEmitter : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private LineRenderer beam;
    [SerializeField] private LayerMask mask;

    [SerializeField] private float fireRate = 2f;
    
    private float _nextTimeToFire;
    
    public void Attack(float damage)
    {
        beam.gameObject.SetActive(true);
        beam.SetPosition(0, firePoint.position);
        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, Mathf.Infinity, mask))
        {
            beam.SetPosition(1, hit.point);
            
            if (hit.collider.gameObject.TryGetComponent(out IDamageable hitObject))
            {
                hitObject.Damage(damage, transform);
            }           
        }
    }
    
    public void StopAttack()
    {
        beam.gameObject.SetActive(false);
    }
}
