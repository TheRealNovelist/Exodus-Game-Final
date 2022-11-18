using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem.Grunt
{
    internal class MoveToPlayer : IState
    {
        private readonly Grunt _grunt;
        private readonly NavMeshAgent _navMeshAgent;
        
        
        public MoveToPlayer(Grunt grunt, NavMeshAgent navMeshAgent)
        {
            _grunt = grunt;
            _navMeshAgent = navMeshAgent;
        }
        
        public void Update()
        {
            _navMeshAgent.SetDestination(_grunt.target.position);
        }

        public void OnEnter()
        {
            _navMeshAgent.enabled = true;
        }

        public void OnExit()
        {
            _navMeshAgent.enabled = false;
        }
        
        
    }
}