using UnityEngine;

namespace EnemySystem.Grunt
{
    internal class Attacking : IState
    {
        private readonly Grunt _grunt;


        public Attacking(Grunt grunt)
        {
            _grunt = grunt;
        }
        
        public void Update()
        {
            Debug.Log("Attacking " + _grunt.target.gameObject.name);
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}
