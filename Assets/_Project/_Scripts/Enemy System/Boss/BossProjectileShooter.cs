using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPosition;
    public float speed;
    public float damage;
    
    public void Attack()
    {
        var bullet = Instantiate(bulletPrefab, spawnPosition.transform.position, spawnPosition.transform.rotation);
        bullet.GetComponent<Bullet>().Init(damage);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
        Destroy(bullet, 10f);
    }
}
