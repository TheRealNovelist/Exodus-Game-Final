namespace EnemySystem
{
    public class Idle : IState
    {
        private readonly BaseEnemy _enemy;

        public Idle(BaseEnemy enemy)
        {
            _enemy = enemy;
        }
        
        public void Update()
        {
            
        }

        public void OnEnter()
        {
            _enemy.EnemyAnimator.SetTrigger("Idle");
        }

        public void OnExit()
        {
            
        }
    }
}