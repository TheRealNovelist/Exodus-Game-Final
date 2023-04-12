using UnityEngine;

namespace EnemySystem.Brute {
    internal class Attacking : IState {
        private readonly Brute _brute;
        private readonly Transform _target;

        private readonly IDamageable _targetDamage;

        private float cooldown = 0f;

        public Attacking(Brute brute, Transform target) {
            _brute = brute;
            _target = target;

            _targetDamage = _target.GetComponent<IDamageable>();
        }

        public void Update() {
            if (cooldown > 0f) {
                cooldown -= Time.deltaTime;
                return;
            }

            //Attack animation
            _targetDamage?.Damage(_brute.damageDealt);
            cooldown = _brute.attackCooldown;
        }

        public void FixedUpdate() { }

        public void OnEnter() { }

        public void OnExit() { }
    }
}