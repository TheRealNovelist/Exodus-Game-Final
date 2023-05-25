using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem.Brute
{
    public class Brute : BaseEnemy
    {
        [Header("Components")]
        [SerializeField] private NavMeshAgent agent;

        [Header("Settings")]
        [SerializeField] private float attackRange = 2f;

        public float attackCooldown = 5f;
        public float damageDealt = 10f;

        protected override void Awake()
        {
            base.Awake();
            
            if (!agent)
                agent = GetComponent<NavMeshAgent>();
        }

        public override void StartStateMachine(float delay = 0f)
        {
            if (IsStateMachineStarted()) return;
            
            var MoveToPlayer = new MoveToTarget(this, agent);
            var Attacking = new Attacking(this);

            AddTransition(MoveToPlayer, Attacking, TargetInRange(attackRange));
            AddAnyTransition(MoveToPlayer, TargetOutRange(attackRange));

            initialState = MoveToPlayer;
            
            Func<bool> TargetInRange(float range) => () => Vector3.Distance(target.position, transform.position) <= range;
            Func<bool> TargetOutRange(float range) => () => Vector3.Distance(target.position, transform.position) > range;
            
            base.StartStateMachine(delay);
        }
    }
    
    
}
