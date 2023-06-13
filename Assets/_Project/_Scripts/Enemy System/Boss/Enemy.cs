using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected EnemyHealth _enemyHealth => GetComponent<EnemyHealth>();
    protected BossRoom _bossRoom;

    public void Init(BossRoom room)
    {
        _bossRoom = room;
    }

    protected virtual void OnEnable()
    {
        if (_bossRoom)
        {
            _enemyHealth.OnDeath += _bossRoom.OnEnemyDied;
        }
    }

    public void Reset()
    {
        _enemyHealth.ResetHealth();
    }

    protected virtual void OnDisable()
    {
        if (_bossRoom)
        {
            _enemyHealth.OnDeath -= _bossRoom.OnEnemyDied;
        }
    }
}