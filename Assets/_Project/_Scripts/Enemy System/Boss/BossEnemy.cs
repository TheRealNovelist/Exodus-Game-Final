using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    protected EnemyHealth _enemyHealth => GetComponent<EnemyHealth>();
    private BossRoom _bossRoom;

    public void Init(BossRoom room)
    {
        _bossRoom = room;
    }

    protected virtual void OnEnable()
    {
        if (_bossRoom)
        {
            _enemyHealth.OnDied += _bossRoom.OnEnemyDied;
        }
    }

    protected virtual void OnDisable()
    {
        _enemyHealth.OnDied -= _bossRoom.OnEnemyDied;
    }
}