using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem.Brute
{
    internal class MoveToPlayer : IState
    {
        private readonly Brute _brute;
        private readonly NavMeshAgent _navMeshAgent;

        private readonly Transform _target;
        
        public MoveToPlayer(Brute brute, NavMeshAgent navMeshAgent, Transform target)
        {
            _brute = brute;
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