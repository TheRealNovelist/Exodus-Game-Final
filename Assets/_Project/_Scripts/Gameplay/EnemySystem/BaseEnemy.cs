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
            if (_spawner) _spawner.EnemyDefeated?.Invoke();
            
            Destroy(gameObject);
        }
        

        public void BindSpawner(ESpawnerSystem spawner)
        {
            _spawner = spawner;
        }

        private void Start()
        {
            RespawnPlayer.OnPlayerStartRespawn += PauseStateMachine;
            RespawnPlayer.OnPlayerFinishedRespawn += ContinueStateMachine;
        }

        private void OnDisable()
        {
            RespawnPlayer.OnPlayerStartRespawn -= PauseStateMachine;
            RespawnPlayer.OnPlayerFinishedRespawn -= ContinueStateMachine;
        }

        private void PauseStateMachine() => _stateMachine.Pause(true);
        private void ContinueStateMachine() => _stateMachine.Pause(false);
    }
}
