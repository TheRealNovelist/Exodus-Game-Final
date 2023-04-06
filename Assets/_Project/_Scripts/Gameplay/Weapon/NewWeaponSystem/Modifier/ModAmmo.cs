using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "New Ammo Modifier", menuName = "Gameplay/Modifier/Ammo")]
    public class ModAmmo : Modifier
    {
        [SerializeField] private int addAmount = 10;
        
        public override WeaponData Modify(WeaponData data)
        {
            Debug.Log("Modified ammo");
            data.magazineSize += addAmount;
            return data;
        }
    }
}
