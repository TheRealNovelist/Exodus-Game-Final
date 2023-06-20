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
        [SerializeField] private AudioManager _audioManager;

        [Header("Range Settings")]
        [SerializeField] private float shootingRange = 10f;
        [SerializeField] private float breakRange = 15f;
        
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
        
        private void OnAnimatorMove()
        {
            Vector3 rootPosition = EnemyAnimator.rootPosition;
            rootPosition.y = agent.nextPosition.y;
            
            transform.position = rootPosition;
            transform.rotation = EnemyAnimator.rootRotation;
            agent.nextPosition = rootPosition;
        }

        public override void StartStateMachine(float delay = 0f)
        {
            if (IsStateMachineStarted()) return;
                    
            var MoveToTarget = new MoveToTargetRM(this, agent);
            var RangedAttack = new RangedAttack(this);
            var Idle = new Idle(this);
            
            AddTransition(MoveToTarget, RangedAttack, IsShootingRange);
            AddTransition(RangedAttack, MoveToTarget, IsOutOfRange);
            
            AddAnyTransition(Idle, () => target == null);
            
            AddTransition(Idle, MoveToTarget, () => target != null && !IsShootingRange());
            AddTransition(Idle, RangedAttack, () => target != null && IsShootingRange());

            initialState = MoveToTarget;
            
            bool IsOutOfRange() => Vector3.Distance(transform.position, target.GetClosestPoint(transform.position)) > breakRange;
            bool IsShootingRange() => Vector3.Distance(transform.position, target.GetClosestPoint(transform.position)) <= shootingRange;

            base.StartStateMachine(delay);
        }

        public void Attack()
        {
            _audioManager.PlayOneShot("EnemyShootAttack");
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Bullet>().Init(damageDealt);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
            Destroy(bullet, 10f);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, breakRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, shootingRange);
        }
    }
}
