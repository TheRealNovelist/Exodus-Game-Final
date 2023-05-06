using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem
{
    internal class MoveToTarget : IState
    {
        private readonly BaseEnemy _enemy;
        private readonly NavMeshAgent _navMeshAgent;
        
        
        public MoveToTarget(BaseEnemy enemy, NavMeshAgent navMeshAgent)
        {
            _enemy = enemy;
            _navMeshAgent = navMeshAgent;
        }
        
        public void Update()
        {
            _navMeshAgent.SetDestination(_enemy.target.position);
        }

        public void OnEnter()
        {
            _navMeshAgent.enabled = true;
            //Start walking animation
        }

        public void OnExit()
        {
            _navMeshAgent.enabled = false;
            //Stop walking animation
        }
    }
}