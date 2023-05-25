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
            if (!_navMeshAgent.SetDestination(_enemy.target.position))
            {
                _enemy.enabled = false;
            }
        }

        public void OnEnter()
        {
            _navMeshAgent.enabled = true;
            
            //Start walking animation
            if (_enemy.EnemyAnimator)
            {
                _enemy.EnemyAnimator.SetTrigger("MoveToTarget");
            }
            
        }

        public void OnExit()
        {
            _navMeshAgent.enabled = false;
            //Stop walking animation
        }
    }
}