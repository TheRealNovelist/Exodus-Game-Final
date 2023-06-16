using UnityEngine;

namespace EnemySystem.Gunner
{
    internal class RangedAttack : IState
    {
        private readonly Gunner _gunner;

        private float _nextTimeToFire;
        private int _ammoCount = 3;

        private int _currentAmmo;
        private float _cooldown;

        private const float fireInterval = 0.06f;

        public RangedAttack(Gunner gunner)
        {
            _gunner = gunner;

            _currentAmmo = _ammoCount;
            _cooldown = _gunner.attackCooldown;
        }
        
        public void Update()
        {
            _gunner.transform.RotateTowards(_gunner.target, freezeX: true, freezeZ: true);
            
            if (_currentAmmo > 0)
            {
                if (!(Time.time >= _nextTimeToFire)) return;
                
                _nextTimeToFire = Time.time + 1f / _gunner.fireRate;
                
                _gunner.EnemyAnimator.SetTrigger("RangedAttack");
                
                _currentAmmo -= 1;
            }
            else
            {
                if (_cooldown > 0)
                {
                    _cooldown -= Time.deltaTime;
                }
                else
                {
                    _currentAmmo = _ammoCount;
                    _cooldown = _gunner.attackCooldown;
                }
            }
        }

        public void OnEnter()
        {
            if (fireInterval < 1f / _gunner.fireRate)
            {
                float multiplier = (1f / _gunner.fireRate) / fireInterval;
                _gunner.EnemyAnimator.SetFloat("ShootTimeMultiplier", multiplier); 
            }
            else
            {
                _gunner.EnemyAnimator.SetFloat("ShootTimeMultiplier", 1f);
            }
            
            _gunner.EnemyAnimator.SetBool("IsRanged", true);
        }

        public void OnExit()
        {
            _gunner.EnemyAnimator.SetBool("IsRanged", false);
        }
    }
}