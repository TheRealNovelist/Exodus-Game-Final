using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem
{
    internal class MoveToPlayer : IState
    {
        private readonly NavMeshAgent _navMeshAgent;

        private readonly Transform _target;
        
        public MoveToPlayer(NavMeshAgent navMeshAgent, Transform target)
        {
            _navMeshAgent = navMeshAgent;
            _target = target;
        }
        
        public void Update()
        {
            _navMeshAgent.SetDestination(_target.position);
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