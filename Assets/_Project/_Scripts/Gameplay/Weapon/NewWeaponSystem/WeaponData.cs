
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
    }

    public enum FiringMode
    {
        SemiAuto,
        FullAuto
    }
}