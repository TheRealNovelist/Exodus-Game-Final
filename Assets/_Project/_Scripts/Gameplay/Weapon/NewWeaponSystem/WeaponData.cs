
using UnityEngine;

namespace WeaponSystem
{
    [System.Serializable]
    public struct WeaponData
    {
        public float damage;
        public float fireRate;
        public float force;
        public float range;
        public int bulletPerShot;
        [Space]
        public float normalReloadTime;
        public float fastReloadTime;
        public float equipTime;
        [Space]
        public int magazineSize;
        public int ammoCostPerBullet;
        
        [Space]
        public FiringMode firingMode;
        
        public static WeaponData operator +(WeaponData x, WeaponData y)
        {
            return new WeaponData
            {
                damage = x.damage + y.damage,
                fireRate = x.fireRate + y.fireRate,
                force = x.force + y.force,
                range = x.range + y.range,
                bulletPerShot = x.bulletPerShot + y.bulletPerShot,
                normalReloadTime = x.normalReloadTime + y.normalReloadTime,
                fastReloadTime = x.fastReloadTime + y.fastReloadTime,
                equipTime = x.equipTime + y.equipTime,
                magazineSize = x.magazineSize + y.magazineSize,
                ammoCostPerBullet = x.ammoCostPerBullet + y.ammoCostPerBullet
            };
        }
    }

    
    
    public enum FiringMode
    {
        Single,
        Hold
    }
}