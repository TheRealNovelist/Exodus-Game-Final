using UnityEngine;

namespace EnemySystem.Brute.Charger
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
            isCoolingDown = true;
            
            if (_charger.EnemyAnimator)
            {
                _charger.EnemyAnimator.SetTrigger("Cooldown");
            }

        }

        public void OnExit()
        {
            
        }
    }
}