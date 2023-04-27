using System.Collections;
using System.Collections.Generic;
using EnemySystem;
using UnityEngine;

public class ES_WaitingState : IState
{
    private float currentTimer;
    private bool waiting = false;

    public ES_WaitingState(float timeCountDown)
    {
        currentTimer = timeCountDown;
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer -= Time.deltaTime;
    }

    public bool FinishedCounting => currentTimer <= 0;

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    void IState.Update()
    {
        Update();
    }
}