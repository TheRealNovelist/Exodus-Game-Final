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
            if (cooldown > 0f)
            {
                cooldown -= Time.deltaTime;
                return;
            }
            
            cooldown = _brute.attackCooldown;
            
            if (_brute.target.TryGetComponent(out IDamageable targetDamage))
            {
                targetDamage.Damage(_brute.damageDealt);
            }
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}
