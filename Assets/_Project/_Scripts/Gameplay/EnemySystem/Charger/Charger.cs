using System;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem.Charger
{
    public class Charger : BaseEnemy
    {
        [Header("Components")] 
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Rigidbody rb;
        
        [Header("Settings")]
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float chargingMultiplier = 0.2f;

        public float chargeTime = 4f;
        public float attackCooldown = 5f;
        public float damageDealt = 10f;
        
        private float painMultiplier = 1f;
        [HideInInspector] public Vector3 attackDirection;

        [HideInInspector] public bool isAttacking;
        private bool hasCollided;
        
        protected override void Awake()
        {
            base.Awake();
            if (!agent)
                agent = GetComponent<NavMeshAgent>();

            if (!rb)
                rb = GetComponent<Rigidbody>();
        }

        public override void StartStateMachine(float delay = 0f)
        {
            if (IsStateMachineStarted()) return;
            
            var MoveToPlayer = new MoveToPlayer(agent, target);
            var Charging = new ChargingAttack(this, target);
            var Attacking = new Attacking(this, rb);
            var Cooldown = new Cooldown(this, attackCooldown);
            
            AddTransition(MoveToPlayer, Charging, TargetInRange());
            AddTransition(Charging, Attacking, () => Charging.isCharged);
            AddAnyTransition(Cooldown, () => hasCollided);
            AddTransition(Cooldown, MoveToPlayer, () => !Cooldown.isCoolingDown);

            initialState = MoveToPlayer;
            
            Func<bool> TargetInRange() => () => Vector3.Distance(target.position, transform.position) <= attackRange;

            base.StartStateMachine(delay);
        }

        public void ReduceDamage(bool isReducing) => painMultiplier = isReducing ? chargingMultiplier : 1f;
        
        public override void Damage(float amount)
        {
            float chargerDamage = amount * painMultiplier;
            
            base.Damage(chargerDamage);
        }

        public void ResetCollision() => hasCollided = false;

        private void OnCollisionEnter(Collision collision)
        {
            if (isAttacking)
            {
                //Stop completely when collided
                rb.velocity = Vector3.zero;
                if (collision.gameObject.GetComponent<IDamageable>() != null)
                {
                    collision.gameObject.GetComponent<IDamageable>().Damage(damageDealt);
                }
                hasCollided = true;
            }
        }
    }
}
