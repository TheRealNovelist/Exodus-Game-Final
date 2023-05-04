using UnityEngine;

namespace EnemySystem.Gunner
{
    internal class Attacking : IState
    {
        private readonly Gunner _gunner;
        private readonly Transform _target;

        public Attacking(Gunner gunner, Transform target)
        {
            _gunner = gunner;
            _target = target;
        }
        
        public void Update()
        {   
            _gunner.transform.LookAt(new Vector3(_target.position.x, _gunner.transform.position.y, _target.position.z));
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}