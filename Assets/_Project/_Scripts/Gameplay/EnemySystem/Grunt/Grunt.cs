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

        public override void StartStateMachine(float delay = 0f)
        {
            var MoveToPlayer = new MoveToPlayer(this, agent);
            var Attacking = new Attacking(this);

            AddTransition(MoveToPlayer, Attacking, TargetInRange());
            AddAnyTransition(MoveToPlayer, TargetOutRange());

            initialState = MoveToPlayer;
            
            Func<bool> TargetInRange() => () => Vector3.Distance(target.position, transform.position) <= attackRange;
            Func<bool> TargetOutRange() => () => Vector3.Distance(target.position, transform.position) > attackRange;
            
            base.StartStateMachine(delay);
        }
    }
}
