using System;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem.Gunner
{
    public class Gunner : BaseEnemy
    {
        [Header("Components")]
        [SerializeField] private NavMeshAgent agent;

        [Header("Settings")]
        [SerializeField] private Vector3 bulletSpawnOffset = new Vector3(0, 1, 0);
        [SerializeField] private float detectionRadius = 2f;
        [SerializeField] private float detectionRange = 10f;

        [SerializeField] private LayerMask detectionMask;
        
        public float attackCooldown = 5f;
        public float damageDealt = 10f;

        protected override void Awake()
        {
            base.Awake();
            
            if (!agent)
                agent = GetComponent<NavMeshAgent>();
        }

        public override void StartStateMachine(float delay = 0f)
        {
            if (IsStateMachineStarted()) return;
                    
            var MoveToPlayer = new MoveToPlayer(agent, target);
            var Attacking = new Attacking(this, target);

            initialState = MoveToPlayer;
            
            AddAnyTransition(Attacking, () => Detect(target));
            AddAnyTransition(MoveToPlayer, () => !Detect(target));

            base.StartStateMachine(delay);
        }


        private bool Detect(Transform desiredTarget)
        {
            if (Physics.SphereCast(transform.position + bulletSpawnOffset, detectionRadius,
                    (desiredTarget.position + bulletSpawnOffset) - (transform.position + bulletSpawnOffset).normalized, 
                    out RaycastHit hit, detectionRange, detectionMask))
            {
                Debug.DrawLine(transform.position, hit.point, Color.red);
                Debug.Log($"{hit.collider.gameObject.name}");

                return hit.transform == desiredTarget;
            }
            
            Debug.DrawLine(transform.position, transform.forward * 10000, Color.green);
            return false;
        }
        
    }
}
