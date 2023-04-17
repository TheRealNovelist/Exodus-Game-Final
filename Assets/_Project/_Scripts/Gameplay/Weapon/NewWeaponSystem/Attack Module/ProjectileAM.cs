using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class ProjectileAM : AttackModule
    {
        public GameObject bulletPrefab;
        public Transform spawnPosition;
        public float speed;

        public override void StartAttack(Weapon weapon, bool consumeAmmo = true)
        {
            ConsumeAmmo(weapon, consumeAmmo);
            
            var bullet = Instantiate(bulletPrefab, spawnPosition.transform.position, spawnPosition.transform.rotation);
            bullet.GetComponent<Bullet>().Init(weapon.data.damage);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
            Destroy(bullet, 10f);
        }

        public override void HoldAttack(Weapon weapon, bool consumeAmmo = true)
        {

        }
    }
}
