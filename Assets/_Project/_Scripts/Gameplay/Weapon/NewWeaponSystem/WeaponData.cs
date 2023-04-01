
using UnityEngine;

namespace WeaponSystem
{
    [System.Serializable]
    public class WeaponData
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float FireRate { get; private set; }
        [field: SerializeField] public float Force { get; private set; }

        [field: SerializeField] public float ReloadTime { get; private set; }

        [field: SerializeField] public int MagazineSize { get; private set; }

        [field: SerializeField] public FiringMode FiringMode { get; private set; } = FiringMode.SemiAuto;
    }

    public enum FiringMode
    {
        SemiAuto,
        FullAuto
    }
}