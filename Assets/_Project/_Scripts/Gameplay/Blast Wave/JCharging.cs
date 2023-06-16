using System.Collections;
using System.Collections.Generic;
using EnemySystem;
using UnityEngine;

public class JCharging : IState
{
    private readonly Juggernaut _enemy;
    private readonly float _maxCooldown;
    private float _currentCooldown = 0;
    
    public bool  isCharged = true;
    public IState NextRandomAttack;
    
    public JCharging(Juggernaut enemy, float maxCooldown)
    {
        _enemy = enemy;
        _maxCooldown = maxCooldown;
    }
    
    // Update is called once per frame
    public void Update()
    {
        if (_currentCooldown <= 0)
        {
            NextRandomAttack = _enemy.attacks[Random.Range(0, _enemy.attacks.Count - 1)];
            Debug.Log(NextRandomAttack);
            isCharged = true;
            return;
        }
        _currentCooldown -= Time.deltaTime;
    }

    public void OnEnter()
    {
        Debug.Log("charge ");

        _currentCooldown = _maxCooldown;
        isCharged = false;
        
    }

    public void OnExit()
    {
    }
}
