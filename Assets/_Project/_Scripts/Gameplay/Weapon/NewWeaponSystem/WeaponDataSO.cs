using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "New Weapon Data", menuName = "Gameplay/Weapon Data")]
    public class WeaponDataSO : ScriptableObject
    {
        public WeaponData data;
    }
}