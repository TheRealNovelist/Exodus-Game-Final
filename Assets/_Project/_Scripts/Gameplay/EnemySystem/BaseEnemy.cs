using System;
using UnityEngine;

namespace EnemySystem
{
    public abstract class BaseEnemy : BaseAI, IDamageable
    {
        [Header("Base Settings")] 
        [SerializeField] private float maxHealth;
        [SerializeField] private bool startOnAwake;
        [SerializeField] private Transform primaryTarget;
        [SerializeField] private Transform player;
        [SerializeField] private bool switchOnAggression;
        
        [HideInInspector] public Transform target;
        
        private float health;
        private ESpawnerSystem _spawner;

        protected override void Awake()
        {
            target = primaryTarget;
            health = maxHealth;
            
            base.Awake();

            if (startOnAwake)
            {
                StartStateMachine();
            }
        }

        public virtual void Damage(float amount, Transform source = null)
        {
            health -= amount;
            Debug.Log("Enemy taken " + amount + " damage");
            Debug.Log("Enemy health " + health);

            if (health <= 0)
            {
                Die();
            }

            if (!switchOnAggression || target == player) return;
            
            Stop();
            StartStateMachine();
            target = player;
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
