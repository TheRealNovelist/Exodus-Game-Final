using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem {
    internal class MoveToPlayer : IState {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Transform _target;
        private readonly Rigidbody _rigidbody;

        public MoveToPlayer(NavMeshAgent agent,Transform target) {
            _target = target;
            _navMeshAgent = agent;
        }

        public void Update() {
                _navMeshAgent.SetDestination(_target.position);
        }

        public void FixedUpdate() {
        }


        public void OnEnter() {
           // _navMeshAgent.enabled = true;
            //Start walking animation
        }

        public void OnExit() {
            _navMeshAgent.enabled = false;
            //Stop walking animation
        }
    }
}