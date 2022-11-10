using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [Header("States")]
    [SerializeField] private State initialState;
    [SerializeField] private State nullState;

    [Header("Modules")]
    [SerializeField] private EnemyAttacker _attacker;
    [SerializeField] private EnemyObserver _observer;
    [SerializeField] private EnemyMovement _movement;

    
    public EnemyAttacker Attacker => _attacker;
    public EnemyObserver Observer => _observer;
    public EnemyMovement Movement => _movement;

    private State currentState;
    
    public void Start()
    {
        TransitionToState(initialState);
    }

    public void SetupBrain()
    {
        
    }

    public void TransitionToState(State newState)
    {
        if (currentState == newState || newState == nullState)
            return;
        
        //Handle exit state before transitioning to next state
        if (currentState != null)
            currentState.ExitState(this);
        
        currentState = newState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        //Update the enemy state
        currentState.UpdateState(this);
    }
}
