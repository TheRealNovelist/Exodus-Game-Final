using System;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem.Gunner
{
    public class Gunner : BaseEnemy
    {
        [Header("Components")]
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Transform bulletSpawnPoint;

        [Header("Detection Settings")]
        [SerializeField] private float detectionRadius = 2f;
        [SerializeField] private float detectionRange = 10f;
        [SerializeField] private LayerMask detectionMask;

        [Header("Weapon Settings")]
        [SerializeField] private GameObject bulletPrefab;
        public float fireRate = 10f;
        public float attackCooldown = 5f;
        public float damageDealt = 10f;
        public float bulletSpeed = 40f;

        private float _timeSinceLastDetect;
        
        protected override void Awake()
        {
            base.Awake();
            
            if (!agent)
                agent = GetComponent<NavMeshAgent>();
        }

        public override void StartStateMachine(float delay = 0f)
        {
            if (IsStateMachineStarted()) return;
                    
            var MoveToTarget = new MoveToTarget(this, agent);
            var RotateToTarget = new RotateToTarget(this);
            var Attacking = new Attacking(this);
            
            AddAnyTransition(Attacking, () => Detect(target));
            AddAnyTransition(RotateToTarget, () => !Detect(target));

            //Enemy will move to a different location if the player is not in range, or cannot detect them for more than 5 seconds
            AddAnyTransition(MoveToTarget, () => !TargetInRange(detectionRange) || _timeSinceLastDetect > 5);

            initialState = MoveToTarget;
            
            bool TargetInRange(float range) => Vector3.Distance(target.position, transform.position) <= range;

            base.StartStateMachine(delay);
        }

        public void Attack()
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Bullet>().Init(damageDealt);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
            Destroy(bullet, 10f);
        }

        private bool Detect(Transform desiredTarget)
        {
            if (Physics.SphereCast(bulletSpawnPoint.position, detectionRadius,
                    (desiredTarget.position - bulletSpawnPoint.position).normalized, 
                    out RaycastHit hit, detectionRange, detectionMask))
            {
                Debug.DrawLine(transform.position, hit.point, Color.red);
                Debug.Log($"{hit.collider.gameObject.name}");

                _timeSinceLastDetect = 0;
                return hit.transform == desiredTarget;
            }

            _timeSinceLastDetect += Time.deltaTime;
            Debug.DrawLine(transform.position, transform.forward * detectionRange, Color.green);
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }
    }
}
