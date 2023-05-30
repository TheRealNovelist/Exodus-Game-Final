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
    
    public void Attack()
    {
        beam.gameObject.SetActive(true);
        beam.SetPosition(0, firePoint.position);
        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, Mathf.Infinity, mask))
        {
            beam.SetPosition(1, hit.point);
        }
    }
    
    public void StopAttack()
    {
        beam.gameObject.SetActive(false);
    }
}
