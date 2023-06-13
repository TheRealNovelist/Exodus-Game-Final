using UnityEngine;

namespace EnemySystem.Brute
{
    internal class Attacking : IState
    {
        private readonly Brute _brute;

        private float cooldown = 0f;
        
        public Attacking(Brute brute)
        {
            _brute = brute;
            
        }
        
        public void Update()
        {
            _brute.transform.RotateTowards(_brute.target);
            
            if (cooldown > 0f)
            {
                cooldown -= Time.deltaTime;
                return;
            }
            
            cooldown = _brute.attackCooldown;
            
            if (_brute.target.TryGetComponent(out IDamageable targetDamage))
            {
                targetDamage.Damage(_brute.damageDealt);
                
                if (_brute.EnemyAnimator)
                {
                    _brute.EnemyAnimator.SetTrigger("Attack");
                }
            }
        }

        public void OnEnter()
        {
            if (_brute.EnemyAnimator)
            {
                _brute.EnemyAnimator.SetBool("IsAttacking", true);
            }
        }

        public void OnExit()
        {
            if (_brute.EnemyAnimator)
            {
                _brute.EnemyAnimator.SetBool("IsAttacking", false);
            }
        }
    }
}
