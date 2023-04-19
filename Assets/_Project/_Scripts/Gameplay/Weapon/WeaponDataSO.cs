using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "New Weapon Data", menuName = "Gameplay/Weapon Data")]
    public class WeaponDataSO : ScriptableObject
    {
        [SerializeField] private WeaponData data;

        public WeaponData GetData() => data;
    }
}