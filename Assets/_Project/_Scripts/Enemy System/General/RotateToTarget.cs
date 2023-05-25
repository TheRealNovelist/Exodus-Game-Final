using UnityEngine;

namespace EnemySystem
{
    public class RotateToTarget : IState
    {
        private readonly BaseEnemy _enemy;

        public RotateToTarget(BaseEnemy enemy)
        {
            _enemy = enemy;
        }
        
        public void Update()
        {
            Vector3 target = _enemy.target.position;
            Vector3 position = _enemy.transform.position;
            
            Quaternion rotation = Quaternion.LookRotation(target - position);
            Quaternion.RotateTowards(_enemy.transform.rotation, rotation, Time.deltaTime * 10f);
            
            //_enemy.transform.LookAt(new Vector3(target.x, position.y, target.z));
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}