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
    public int pointsCount;
    public float MaxRadius;
    public float BlastSpeed;
    public float startWidth;
    public float force;
    public LayerMask blastMask;

    protected BossRoom _bossRoom;


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
        var Punching = new JPunching();
       var Throwing = new JThrowing(this);


        attacks.Add(Blasting);
       // attacks.Add(Punching);
       // attacks.Add(Throwing);

        // AddTransition(Charging, attacks[Random.Range(0, attacks.Count - 1)], () => Charging.isCharged);
        AddTransition(Charging, Blasting, () => Charging.isCharged && Charging.NextRandomAttack == Blasting);
      //  AddTransition(Charging, Throwing, () => Charging.isCharged && Charging.NextRandomAttack == Throwing);
        AddTransition(Blasting, Charging, () => Blasting.Blasted);
     //   AddTransition(Throwing, Charging, () => Blasting.Blasted);
        
        
        initialState = Charging;
        _stateMachine.SetState(Charging);
        base.StartStateMachine();
    }
    
}