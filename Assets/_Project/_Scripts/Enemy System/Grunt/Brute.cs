using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace EnemySystem.Brute
{
    public class Brute : BaseEnemy
    {
        [Header("Components")]
        [SerializeField] protected NavMeshAgent agent;

        [Header("Settings")]
        [SerializeField] private float attackRange = 5f;

        public float attackCooldown = 5f;
        public float damageDealt = 10f;

        [HideInInspector] public float cooldownTime;

        protected MoveToTarget MoveToTarget;
        
        protected override void Awake()
        {
            if (!agent)
                agent = GetComponent<NavMeshAgent>();

            base.Awake();
        }

        private void OnAnimatorMove()
        {
            Vector3 rootPosition = EnemyAnimator.rootPosition;
            rootPosition.y = agent.nextPosition.y;
            
            transform.position = rootPosition;
            transform.rotation = EnemyAnimator.rootRotation;
            agent.nextPosition = rootPosition;
        }

        public override void StartStateMachine(float delay = 0f)
        {
            if (IsStateMachineStarted()) return;

                MoveToTarget = new MoveToTarget(this, agent);
            var Attacking = new Attacking(this);
            var Idle = new Idle(this);

            AddTransition(MoveToTarget, Attacking, TargetInRange);
            AddTransition(Attacking, MoveToTarget, TargetOutRange);
            
            AddAnyTransition(Idle, () => target == null);
            
            AddTransition(Idle, MoveToTarget, () => target != null && TargetOutRange());
            AddTransition(Idle, Attacking, () => target != null && TargetInRange());

            initialState = MoveToTarget;
            
            bool TargetInRange() => Vector3.Distance(target.position, transform.position) <= attackRange;
            bool TargetOutRange() => Vector3.Distance(target.position, transform.position) > attackRange;
            
            base.StartStateMachine(delay);
        }

        protected override void OnStateMachineUpdate()
        {
            base.OnStateMachineUpdate();
            
            if (cooldownTime > 0f)
            {
                cooldownTime -= Time.deltaTime;
            }
        }

        public void Attack()
        {
            if (target.TryGetComponent(out IDamageable targetDamage))
            {
                targetDamage.Damage(damageDealt);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
    
    
}
