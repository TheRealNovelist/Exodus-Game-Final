using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem.Brute.Charger
{
    internal class ChargeAttack : IState
    {
        private readonly Charger _charger;
        private readonly NavMeshAgent _agent;

        private Animator _animator;

        private Vector2 _velocity;
        private Vector2 _smoothDeltaPosition;

        private Vector3 destination;

        private bool bite;
        
        public ChargeAttack(Charger charger, NavMeshAgent agent)
        {
            _charger = charger;
            _agent = agent;
            
            _animator = _charger.EnemyAnimator;

            _animator.applyRootMotion = true;
            _agent.updatePosition = false;
            _agent.updateRotation = true;
        }
        
        public void Update()
        {
            Transform transform = _charger.transform;

            Vector3 targetPosition = destination;
            targetPosition.y = transform.position.y;
            
            if (!_agent.SetDestination(targetPosition))
            {
                _charger.enabled = false;
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

            if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
            {
                _charger.EnemyAnimator.SetTrigger("Cooldown");
                _charger.initiatedChargeAttack = false;
            }
            
            if (!bite)
            {
                bite = true;
                
                Collider[] colliders = new Collider[] { };
            
                if (Physics.OverlapSphereNonAlloc(_charger.chargeCastOrigin.position, _charger.chargeCastRadius,
                        colliders, _charger.chargeMask) > 0)
                {
                    if (_charger.EnemyAnimator)
                    {
                        _charger.EnemyAnimator.SetTrigger("Bite");
                    }
                }
            }
        }

        public void OnEnter()
        {
            _agent.isStopped = false;
            destination = _charger.target.position;
        }

        public void OnExit()
        {
            _agent.isStopped = true;
        }
    }
}