using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

public class ProjectileAM : AttackModule
{
    public GameObject bulletPrefab;
    public Transform spawnPosition;
    public float speed;

    public override void Attack(WeaponData data)
    {
        var bullet = Instantiate(bulletPrefab, spawnPosition.transform.position, spawnPosition.transform.rotation);
        bullet.GetComponent<Bullet>().Init(data.damage);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
        Destroy(bullet, 10f);
    }
}
