using UnityEngine;

//attach to Mesh of player to detect collision with ammo crate
namespace Old
{
    public class AmmoCrate : MonoBehaviour
    {
        [SerializeField] private CrateData _crateData; //reference to crate data--data container-scriptableObject
        private AmmoManager _ammoManager; //reference to AmmoManager class
    
        private void Start()
        {
            _ammoManager = GameObject.Find("Player 1").GetComponent<AmmoManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other) return;
        
            AddAmmo(_crateData.ammoAmount); //set amount of ammo in crate equal to value in scriptable object pass in
            Destroy(gameObject);
        }

        void AddAmmo(int ammoAmount)
        {
            _ammoManager.ammoPlayerCurrentHave += ammoAmount;
            //if ammoPlayerCurrentHave is bigger than maxAmount of ammo they can carry then set ammoPlayerCurrentHave to _maxAmmoPlayerCanCarry
            if (_ammoManager.ammoPlayerCurrentHave >= _ammoManager._maxAmmoPlayerCanCarry)
                _ammoManager.ammoPlayerCurrentHave = _ammoManager._maxAmmoPlayerCanCarry;
        }
    }
}
