using System.Collections;
using System.Collections.Generic;
using EnemySystem;
using UnityEngine;

public class ES_WaitingState : IState
{
    private float currentTimer, timerDuration;
    private bool waiting = false;

    public ES_WaitingState(float timeCountDown)
    {
        timerDuration = timeCountDown;
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer -= Time.deltaTime;
    }

    public bool FinishedCounting => currentTimer <= 0;

    public void OnEnter()
    {
        currentTimer = timerDuration;
    }

    public void OnExit()
    {
    }

    void IState.Update()
    {
        Update();
    }
}