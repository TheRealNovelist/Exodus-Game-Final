using UnityEngine;

namespace EnemySystem.Charger
{
    internal class ChargingAttack : IState
    {
        private readonly Charger _charger;

        private float _chargeTime;

        public bool isCharged = false;

        private Color defaultColor;

        public ChargingAttack(Charger charger)
        {
            _charger = charger;
        }

        public void Update()
        {
            var chargerTransform = _charger.transform;

            if (_chargeTime <= 0f)
            {
                _charger.SetState(_charger.Attacking);
                
                if (_charger.EnemyAnimator)
                {
                    _charger.EnemyAnimator.SetTrigger("FinishedCharging");
                }
                
                return;
            }

            var target = _charger.target.position;
            chargerTransform.LookAt(new Vector3(target.x, _charger.transform.position.y, target.z));
            _chargeTime -= Time.deltaTime;
        }

        public void OnEnter()
        {
            _chargeTime = _charger.chargeTime;
            isCharged = false;
            _charger.ReduceDamage(true);

            if (_charger.EnemyAnimator)
            {
                _charger.EnemyAnimator.SetTrigger("ChargingAttack");
            }

        }

        public void OnExit()
        {
            _charger.ReduceDamage(false);
            _charger.attackDirection = _charger.transform.forward;
        }
    }
}