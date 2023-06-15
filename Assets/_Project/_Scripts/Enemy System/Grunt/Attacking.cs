using UnityEngine;

namespace EnemySystem.Brute
{
    internal class Attacking : IState
    {
        private readonly Brute _brute;
        
        public Attacking(Brute brute)
        {
            _brute = brute;
            
        }
        
        public void Update()
        {
            _brute.transform.RotateTowards(_brute.target);

            if (_brute.cooldown > 0f) return;
            
            _brute.cooldown = _brute.attackCooldown;
            
            if (_brute.EnemyAnimator)
            {
                _brute.EnemyAnimator.SetTrigger("Attack");
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
