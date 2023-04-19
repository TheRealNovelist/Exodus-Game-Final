using System;
using UnityEngine;

namespace EnemySystem
{
    public abstract class BaseEnemy : BaseAI, IDamageable
    {
        [Header("Base Settings")] 
        public float maxHealth;
        
        public Transform target;

        private float health;

        private ESpawnerSystem _spawner;

        public virtual void Damage(float amount)
        {
            health -= amount;

            if (health <= 0)
            {
                Die();
            }
        }

        public virtual void Die()   
        {
            _spawner.EnemyDie();
            Destroy(gameObject);
        }
        

        public void Init(ESpawnerSystem spawner)
        {
            _spawner = spawner;
        }
    }
}
