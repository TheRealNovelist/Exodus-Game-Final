using UnityEngine;

namespace EnemySystem.Charger
{
    internal class Cooldown : IState
    {
        private readonly Charger _charger;
        private readonly float _cooldownTime;

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

            _charger.SetState(_charger.MoveToTarget);
        }

        public void OnEnter()
        {
            cooldown = _cooldownTime;
            _charger.ResetCollision();

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