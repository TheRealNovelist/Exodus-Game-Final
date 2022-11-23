using UnityEngine;

namespace EnemySystem.Charger
{
    internal class ChargingAttack : IState
    {
        private readonly Charger _charger;
        private readonly Transform _target;

        private float _chargeTime;

        public bool isCharged = false;
        
        public ChargingAttack(Charger charger, Transform target)
        {
            _charger = charger;
            _target = target;
        }

        public void Update()
        {
            var chargerTransform = _charger.transform;
            
            if (_chargeTime <= 0f)
            {
                isCharged = true;
                return;
            }
            
            chargerTransform.LookAt(new Vector3(_target.position.x, _charger.transform.position.y, _target.position.z));
            _chargeTime -= Time.deltaTime;
        }

        public void OnEnter()
        {
            _chargeTime = _charger.chargeTime;
            isCharged = false;
            _charger.ReduceDamage(true);
        }

        public void OnExit()
        {
            _charger.ReduceDamage(false);
            _charger.attackDirection = _charger.transform.forward;
        }
    }
}