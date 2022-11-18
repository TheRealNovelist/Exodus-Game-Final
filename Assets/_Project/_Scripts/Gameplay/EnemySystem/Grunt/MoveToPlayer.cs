using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem.Grunt
{
    internal class MoveToPlayer : IState
    {
        private readonly Grunt _grunt;
        private readonly NavMeshAgent _navMeshAgent;

        private readonly Transform _target;
        
        public MoveToPlayer(Grunt grunt, NavMeshAgent navMeshAgent, Transform target)
        {
            _grunt = grunt;
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