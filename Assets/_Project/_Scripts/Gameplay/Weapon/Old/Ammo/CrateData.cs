using UnityEngine;

namespace Old
{
    [CreateAssetMenu(fileName = "New crate", menuName = "Crate")]
    public class CrateData : ScriptableObject
    {
        public string crateAmmoType; //type of ammo that will be store in side crate
        public int ammoAmount;
        public int ammoType; //store ammo type by int
    }
}
