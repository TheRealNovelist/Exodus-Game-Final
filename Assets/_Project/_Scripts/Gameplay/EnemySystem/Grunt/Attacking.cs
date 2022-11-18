using UnityEngine;

namespace EnemySystem.Grunt
{
    internal class Attacking : IState
    {
        private readonly Grunt _grunt;
        private readonly Transform _target;

        public Attacking(Grunt grunt, Transform target)
        {
            _grunt = grunt;
            _target = target;
        }
        
        public void Update()
        {
            Debug.Log("Attacking " + _target.gameObject.name);
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}
