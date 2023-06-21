using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem.Charger
{
    public class Charger : BaseEnemy
    {
        [Header("Components")] 
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private AudioManager _audioManager;
        
        [Header("Settings")]
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float chargingMultiplier = 0.2f;

        public float chargeTime = 4f;
        public float attackCooldown = 5f;
        public float damageDealt = 10f;
        public float attackForce = 10f;
        
        private float painMultiplier = 1f;
        [HideInInspector] public Vector3 attackDirection;

        [HideInInspector] public bool isAttacking;
        private bool hasCollided;

        public MoveToTarget MoveToTarget;
        public Attacking Attacking;
        
        protected override void Awake()
        {
            if (!agent)
                agent = GetComponent<NavMeshAgent>();

            if (!rb)
                rb = GetComponent<Rigidbody>();
            base.Awake();
        }

        [Button]
        public override void StartStateMachine(float delay = 0f)
        {
            if (IsStateMachineStarted()) return;
            
                MoveToTarget = new MoveToTarget(this, agent);
            var Charging = new ChargingAttack(this);
                Attacking = new Attacking(this, rb);
            var Cooldown = new Cooldown(this, attackCooldown);
            
            AddTransition(MoveToTarget, Charging, TargetInRange);
            AddAnyTransition(Cooldown, () => hasCollided);

            initialState = MoveToTarget;
            
            bool TargetInRange() => Vector3.Distance(target.GetClosestPoint(transform.position), transform.position) <= attackRange;

            base.StartStateMachine(delay);
        }

        public void ReduceDamage(bool isReducing)
        {
            float damageMultiplier = isReducing ? chargingMultiplier : 1f;
            Health.SetDamageMultiplier(damageMultiplier);
        }
        
        public void ResetCollision() => hasCollided = false;

        private void OnCollisionEnter(Collision collision)
        {
            if (!isAttacking) return;
            
            //Stop completely when collided
            rb.velocity = Vector3.zero;
            if (collision.gameObject.GetComponent<IDamageable>() != null)
            {
                _audioManager.PlayOneShot("EnemyDashAttack");
                collision.gameObject.GetComponent<IDamageable>().Damage(damageDealt);
            }
            hasCollided = true;
        }
    }
}
