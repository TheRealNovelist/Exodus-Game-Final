using UnityEngine;

namespace EnemySystem.Charger
{
    public class Attacking : IState
    {
        private readonly Charger _charger;
        private readonly Rigidbody _rigidbody;
        
        public Attacking(Charger charger, Rigidbody rigidbody)
        {
            _charger = charger;
            _rigidbody = rigidbody;
        }
        
        public void Update()
        {
            
        }

        public void OnEnter()
        {
            _charger.isAttacking = true;
            _rigidbody.AddForce(_charger.attackDirection * (_charger.attackForce * 100f), ForceMode.Impulse);
        }

        public void OnExit()
        {
            _charger.isAttacking = false;
        }
    }
}