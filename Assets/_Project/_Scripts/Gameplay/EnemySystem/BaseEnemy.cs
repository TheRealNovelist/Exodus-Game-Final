using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemySystem
{
    public class BaseEnemy : MonoBehaviour
    {
        protected StateMachine _stateMachine;

        protected IState initialState;

        protected virtual void Awake()
        {
            _stateMachine = new StateMachine();
        }

        public void StartStateMachine()
        {
            _stateMachine.SetState(initialState);
        }
        
        private void Update() => _stateMachine.Tick();

        protected void AddAnyTransition(IState to, Func<bool> condition) =>
            _stateMachine.AddAnyTransition(to, condition);
        
        protected void AddTransition(IState to, IState from, Func<bool> condition) =>
            _stateMachine.AddTransition(to, from, condition);
    }
}
