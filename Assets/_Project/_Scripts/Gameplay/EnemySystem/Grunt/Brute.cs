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
            
            var MoveToPlayer = new MoveToPlayer(agent, target);
            var Attacking = new Attacking(this, target);

            AddTransition(MoveToPlayer, Attacking, TargetInRange());
            AddAnyTransition(MoveToPlayer, TargetOutRange());

            initialState = MoveToPlayer;
            
            Func<bool> TargetInRange() => () => Vector3.Distance(target.position, transform.position) <= attackRange;
            Func<bool> TargetOutRange() => () => Vector3.Distance(target.position, transform.position) > attackRange;
            
            base.StartStateMachine(delay);
        }
    }
    
    
}
