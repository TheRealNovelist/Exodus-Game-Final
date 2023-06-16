using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem
{
    internal class MoveToTargetRM : IState
    {
        private readonly BaseEnemy _enemy;
        private readonly NavMeshAgent _agent;

        private Animator _animator;

        private Vector2 _velocity;
        private Vector2 _smoothDeltaPosition;
        
        public MoveToTargetRM(BaseEnemy enemy, NavMeshAgent agent)
        {
            _enemy = enemy;
            _agent = agent;
            _animator = _enemy.EnemyAnimator;

            _animator.applyRootMotion = true;
            _agent.updatePosition = false;
            _agent.updateRotation = true;
        }
        
        public void Update()
        {
            Transform transform = _enemy.transform;

            Vector3 targetPosition = _enemy.target.position;
            targetPosition.y = transform.position.y;
            
            if (!_agent.SetDestination(targetPosition))
            {
                _enemy.enabled = false;
            }
            
            Vector3 worldDeltaPosition = _agent.nextPosition - transform.position;
            worldDeltaPosition.y = 0;

            float dx = Vector3.Dot(transform.right, worldDeltaPosition);
            float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
            Vector2 deltaPosition = new Vector2(dx, dy);

            float smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
            _smoothDeltaPosition = Vector2.Lerp(_smoothDeltaPosition, deltaPosition, smooth);
            _velocity = _smoothDeltaPosition / Time.deltaTime;

            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                _velocity = Vector2.Lerp(Vector2.zero,_velocity, _agent.remainingDistance / _agent.stoppingDistance);
            }

            bool shouldMove = _velocity.magnitude > 0.5f && _agent.remainingDistance > _agent.stoppingDistance;
            
            _animator.SetBool("Move", shouldMove);
            _animator.SetFloat("Locomotion", _velocity.magnitude);

            float deltaMagnitude = worldDeltaPosition.magnitude;
            if (deltaMagnitude > _agent.radius / 2f)
            {
                transform.position = Vector3.Lerp(_animator.rootPosition, _agent.nextPosition, smooth);
            }
        }

        public void OnEnter()
        {
            _agent.enabled = true;
        }

        public void OnExit()
        {
            //Stop walking animation
            _animator.SetBool("Move", false);
            _agent.enabled = false;
        }
    }
}