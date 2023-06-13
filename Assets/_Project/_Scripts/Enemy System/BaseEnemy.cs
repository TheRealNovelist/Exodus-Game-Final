using System;
using UnityEngine;

namespace EnemySystem
{
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(TargetLocator))]
    public abstract class BaseEnemy : BaseAI
    {
        [Header("Base Settings")]
        [SerializeField] private bool startOnAwake;
        
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private int reward = 20;
        
        protected EnemyHealth Health => GetComponent<EnemyHealth>();
        protected TargetLocator TargetLocator => GetComponent<TargetLocator>();
        public Transform target => TargetLocator.Target;
        
        public Animator EnemyAnimator;
        private EnemySpawnerSystem _spawner;
        
        protected override void Awake()
        {
            base.Awake();

            if (startOnAwake)
            {
                StartStateMachine();
            }

            Health.OnDeath += OnDeath;
        }
        
        public virtual void OnDeath()
        {
            EnemyAnimator.SetTrigger("Death");
            GetComponent<Collider>().enabled = false;
            Stop();
            
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
