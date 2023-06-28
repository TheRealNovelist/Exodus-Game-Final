using System.Collections;
using System.Collections.Generic;
using NodeCanvas.StateMachines;
using UnityEngine;

public class BossGunner : Enemy
{
    [SerializeField] private FSMOwner _fsmOwner;
    public IEnumerator Initialize()
    {
        yield return new WaitForSeconds(5f);
    }
    
    public void OnDeath()
    {
        if(_bossRoom)     _bossRoom.OnRoomPassed?.Invoke();
        WinLoseCondition.OnBossDefeated?.Invoke();
        gameObject.SetActive(false);
    }

    protected override void OnEnable()
    {
        _fsmOwner.enabled = true;

        base.OnEnable();
    }

    protected override void OnDisable()
    {
        _fsmOwner.enabled = false;

        base.OnDisable();
    }
}