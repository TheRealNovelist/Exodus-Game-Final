using UnityEngine;

namespace EnemySystem.Gunner
{
    internal class Attacking : IState
    {
        private readonly Gunner _gunner;

        private float _nextTimeToFire;
        private int _ammoCount = 3;

        private int _currentAmmo;
        private float _cooldown;

        public Attacking(Gunner gunner)
        {
            _gunner = gunner;

            _currentAmmo = _ammoCount;
            _cooldown = _gunner.attackCooldown;
        }
        
        public void Update()
        {
            var target = _gunner.target.position;
            _gunner.transform.LookAt(new Vector3(target.x, _gunner.transform.position.y, target.z));
            if (_currentAmmo > 0)
            {
                if (!(Time.time >= _nextTimeToFire)) return;
                
                _nextTimeToFire = Time.time + 1f / _gunner.fireRate;
                _gunner.Attack();
                
                _currentAmmo -= 1;
                Debug.Log("Attacking");
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
            
        }

        public void OnExit()
        {
            
        }
    }
}