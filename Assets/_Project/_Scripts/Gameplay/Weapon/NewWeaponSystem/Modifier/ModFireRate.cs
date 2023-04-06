using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "New Fire Rate Modifier", menuName = "Gameplay/Modifier/Fire Rate")]
    public class ModFireRate : Modifier
    {
        [SerializeField] private float fireRateFactor = 1.5f;
        
        public override WeaponData Modify(WeaponData data)
        {
            Debug.Log("Modified fire rate");
            data.fireRate *= fireRateFactor;
            return data;
        }
    }
}