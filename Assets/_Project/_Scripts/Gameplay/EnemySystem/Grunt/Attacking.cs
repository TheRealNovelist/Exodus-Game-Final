using UnityEngine;

namespace EnemySystem.Grunt
{
    internal class Attacking : IState
    {
        private readonly Grunt _grunt;
        private readonly Transform _target;

        private readonly IDamageable _targetDamage;
        
        private float cooldown = 0f;
        
        public Attacking(Grunt grunt, Transform target)
        {
            _grunt = grunt;
            _target = target;

            _targetDamage = _target.GetComponent<IDamageable>();
        }
        
        public void Update()
        {
            if (cooldown > 0f)
            {
                cooldown -= Time.deltaTime;
                return;
            }
            
            _targetDamage?.Damage(_grunt.damage);
            cooldown = _grunt.attackCooldown;
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}
