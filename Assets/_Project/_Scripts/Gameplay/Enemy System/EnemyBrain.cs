using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private State nullState;
    [SerializeField] private State initialState;
    
    public EnemyAttacker Attacker;
    public EnemyObserver Observer;
    public EnemyMovement Movement;

    private State currentState;
    
    public void Awake()
    {
        
    }

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
