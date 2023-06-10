using System.Collections;
using System.Collections.Generic;
using EnemySystem;
using UnityEngine;

public class JCharging : IState
{
    private readonly Juggernaut _enemy;
    private readonly float _maxCooldown;
    private float _currentCooldown = 0;
    
    public bool  isCharged = false;

    public IState AttackState;
    
    public JCharging(Juggernaut enemy, float maxCooldown)
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
            int rand = Random.Range(0, _enemy.Attacks.Count);
            AttackState = _enemy.Attacks[rand];
            return;
        }
        
        _currentCooldown -= Time.deltaTime;
    }

    public void OnEnter()
    {
        _currentCooldown = _maxCooldown;
        isCharged = false;
        
    }

    public void OnExit()
    {

    }
    
}
