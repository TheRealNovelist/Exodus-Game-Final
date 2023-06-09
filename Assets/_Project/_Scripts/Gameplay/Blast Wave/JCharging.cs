using System.Collections;
using System.Collections.Generic;
using EnemySystem;
using UnityEngine;

public class JCharging : IState
{
    private readonly BaseEnemy _enemy;
    private readonly float _maxCooldown;
    private float _currentCooldown = 0;
    
    public bool  isCharged = true;
    
    public JCharging(BaseEnemy enemy, float maxCooldown)
    {
        Debug.Log("charge ");

        _enemy = enemy;
        _maxCooldown = maxCooldown;
    }
    
    // Update is called once per frame
    public void Update()
    {
        if (_currentCooldown <= 0)
        {
            isCharged = true;
            return;
        }
        
        _currentCooldown -= Time.deltaTime;
    }

    public void OnEnter()
    {
        _currentCooldown = _maxCooldown;
        isCharged = false;
        
        Debug.Log("charge eneter");
    }

    public void OnExit()
    {
        Debug.Log("charge exit");

    }
    
}
