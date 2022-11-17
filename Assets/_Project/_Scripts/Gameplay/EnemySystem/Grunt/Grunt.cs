using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem.Grunt
{
    public class Grunt : BaseEnemy
    {
        [Header("Components")]
        [SerializeField] private NavMeshAgent agent;

        [Header("Settings")]
        [SerializeField] private float attackRange = 2f;
        
        [SerializeField] private Transform target;
        
        protected override void Awake()
        {
            base.Awake();

            var MoveToPlayer = new MoveToPlayer(this, agent, target);
            var Attacking = new Attacking(this, target);

            AddTransition(MoveToPlayer, Attacking, TargetInRange());
            
            AddAnyTransition(MoveToPlayer, TargetOutRange());

            initialState = MoveToPlayer;
            StartStateMachine();

            Func<bool> TargetInRange() => () => Vector3.Distance(target.position, transform.position) <= attackRange;
            Func<bool> TargetOutRange() => () => Vector3.Distance(target.position, transform.position) > attackRange;
        }
    }
}
