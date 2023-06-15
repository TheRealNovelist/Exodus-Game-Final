using UnityEngine;

namespace EnemySystem.Brute.Charger
{
    internal class Charging : IState
    {
        private readonly Charger _charger;
        
        public Charging(Charger charger)
        {
            _charger = charger;
        }

        public void Update()
        {
            _charger.transform.RotateTowards(_charger.target, freezeX: true, freezeZ: true);
        }

        public void OnEnter()
        {
            _charger.ReduceDamage(true);
            if (_charger.EnemyAnimator)
            {
                _charger.EnemyAnimator.SetTrigger("StartAttack");
            }
        }

        public void OnExit()
        {
            _charger.ReduceDamage(false);
        }
    }
}