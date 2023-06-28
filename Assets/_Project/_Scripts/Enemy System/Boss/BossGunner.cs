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
        _fsmOwner.gameObject.SetActive(false);
        if(_bossRoom)     _bossRoom.OnRoomPassed?.Invoke();
        WinLoseCondition.OnBossDefeated?.Invoke();
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