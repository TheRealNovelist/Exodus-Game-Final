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
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private int reward = 20;
        
        private float health;
        private EnemySpawnerSystem _spawner;
        public Animator EnemyAnimator;

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
//            Debug.Log("Enemy taken " + amount + " damage");
 //           Debug.Log("Enemy health " + health);

            if (health <= 0)
            {
                Die();
            }

            if (switchOnAggression && source == player)
            {
                Debug.Log(source);
                if (target == player)
                    return;
                
                Stop();
                StartStateMachine();
                target = player;
            }
        }

        public virtual void Die()   
        {
            audioManager.PlayOneShot("EnemyDieSound");
            if (_spawner) _spawner.EnemyDefeated?.Invoke(this);
            CoinManager.Instance.GainCoin(reward);
            //Audio
            audioManager.PlayOneShot("EnemyCoinCollect");
            Destroy(gameObject);
        }
        

        public void BindSpawner(EnemySpawnerSystem spawner)
        {
            _spawner = spawner;
        }
        

        protected override void OnDisable()
        {
            base.OnDisable();
            
            EnemyAnimator.SetTrigger("Disable");
        }
        
    }
}
