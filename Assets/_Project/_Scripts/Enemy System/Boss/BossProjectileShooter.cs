using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BossProjectileShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPosition;
    public float speed;
    public float damage;

    private Vector3 initRotation;
    
    private void Awake()
    {
        initRotation = transform.localRotation.eulerAngles;
    }

    public void Attack()
    {
        var bullet = Instantiate(bulletPrefab, spawnPosition.transform.position, spawnPosition.transform.rotation);
        bullet.GetComponent<Bullet>().Init(damage);
        bullet.GetComponent<Rigidbody>().AddForce(spawnPosition.transform.forward * speed, ForceMode.Impulse);
        Destroy(bullet, 10f);
    }

    public void ResetRotation()
    {
        transform.DOLocalRotate(initRotation, 2f);
    }
}