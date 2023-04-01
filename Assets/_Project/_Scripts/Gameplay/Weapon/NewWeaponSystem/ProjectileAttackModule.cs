using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

public class ProjectileAttackModule : AttackModule
{
    public GameObject bulletPrefab;
    public Transform spawnPosition;
    public float speed;

    public override void Attack(float damage)
    {
        var bullet = Instantiate(bulletPrefab, spawnPosition.transform.position, spawnPosition.transform.rotation);
        bullet.GetComponent<Bullet>().Init(damage);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
        Destroy(bullet, 10f);
    }
}
