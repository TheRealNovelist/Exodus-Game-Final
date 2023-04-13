using UnityEngine;

namespace EnemySystem.Charger
{
    internal class Cooldown : IState
    {
        private readonly Charger _charger;
        private readonly float _cooldownTime;

        public bool isCoolingDown;

        private float cooldown;
        
        public Cooldown(Charger charger, float cooldownTime)
        {
            _charger = charger;
            _cooldownTime = cooldownTime;
        }
        
        public void Update()
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
                return;
            }

            isCoolingDown = false;
        }

        public void OnEnter()
        {
            cooldown = _cooldownTime;
            _charger.ResetCollision();
            isCoolingDown = true;
        }

        public void OnExit()
        {
            
        }
    }
}