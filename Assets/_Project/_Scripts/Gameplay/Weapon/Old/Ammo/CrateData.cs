using UnityEngine;

namespace Old
{
    [CreateAssetMenu(fileName = "New crate", menuName = "Crate")]
    public class CrateData : ScriptableObject
    {
        public string crateName; //type of ammo that will be store in side crate
        public int ammoAmount;
    }
}
