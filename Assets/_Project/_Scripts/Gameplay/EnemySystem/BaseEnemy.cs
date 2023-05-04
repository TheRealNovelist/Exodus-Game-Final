using System;
using UnityEngine;

namespace EnemySystem
{
    public abstract class BaseEnemy : BaseAI, IDamageable
    {
        [Header("Base Settings")] 
        public float maxHealth;
        [SerializeField] private bool startOnAwake;
        
        public Transform target;

        private float health;

        private ESpawnerSystem _spawner;

        protected override void Awake()
        {
            base.Awake();

            if (startOnAwake)
            {
                StartStateMachine();
            }
        }

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
            if (_spawner) _spawner.EnemyDefeated?.Invoke();
            
            Destroy(gameObject);
        }
        

        public void BindSpawner(ESpawnerSystem spawner)
        {
            _spawner = spawner;
        }
    }
}
