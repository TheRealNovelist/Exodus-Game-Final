using System.Collections;
using System.Collections.Generic;
using EnemySystem;
using UnityEngine;

public class Juggernaut : BaseEnemy
{
    [SerializeField] private float CooldownTime = 10f;
    public int DamageDealth = 20;
    public float ChargeTime = 4f;

    [Header("Blaster")] public Transform BlastPos;
    public BlastWave _blastWave;
    public BossRoom _bossRoom;

    public GameObject Shield;

    public BlastWaveDataSO BlastSO;

    public int MaxSmallBlaster = 2;
    public List<SmallBlasterSlot> BlasterSlots = new List<SmallBlasterSlot>();
    public SmallBlaster SmallBlaster;
    [Header("Throwing")] public Transform ThrowPoint;
    public float ThrowWaitTime = 3f;

    public void Init(BossRoom room)
    {
        _bossRoom = room;
    }
    
    public void Reset()
    {
        Health.ResetHealth();
    }

    public override void OnDeath()
    {
        if (_bossRoom)
        {
           _bossRoom.OnEnemyDied?.Invoke();
        }
        
        base.OnDeath();
    }
    
    public List<IState> attacks = new List<IState>();

    public override void StartStateMachine(float delay = 0f)
    {
        if (IsStateMachineStarted()) return;

        var Charging = new JCharging(this, CooldownTime);
        var Blasting = new JBlasting(this); 
        var Spawning = new JSpawning(this);
       var Throwing = new JThrowing(this);


       // attacks.Add(Blasting);
        attacks.Add(Spawning);
       // attacks.Add(Throwing);

        AddTransition(Charging, Blasting, () => Charging.isCharged && Charging.NextRandomAttack == Blasting);
      //  AddTransition(Charging, Throwing, () => Charging.isCharged && Charging.NextRandomAttack == Throwing);
        AddTransition(Charging, Spawning, () => Charging.isCharged && Charging.NextRandomAttack == Spawning);
        AddTransition(Blasting, Charging, () => Blasting.WavedShocked);
        AddTransition(Spawning, Charging, () => Spawning.Spawned);
     //   AddTransition(Throwing, Charging, () => Blasting.Blasted);
        
        
        initialState = Charging;
        _stateMachine.SetState(Charging);
        base.StartStateMachine();
    }
    
}