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
    
    public List<IState> Attacks = new List<IState>();

    [Header("Throwing")] public Transform ThrowingHand;
    public float ThrowForce = 40f;

    public override void StartStateMachine(float delay = 0f)
    {
        if (IsStateMachineStarted()) return;

        Physics.IgnoreLayerCollision(6, 9);


        var Charging = new JCharging(this, CooldownTime);
        var Blasting = new JBlasting(this);
        var Punching = new JPunching();
        var Throwing = new JThrowing(this);
        
        Attacks.Add(Blasting);
        Attacks.Add(Throwing);

        initialState = Charging;
        
        //AddTransition(Charging, Attacks[rand], () => Charging.isCharged);
        AddTransition(Charging, Blasting, () => Charging.isCharged && Charging.AttackState == Blasting);
        AddTransition(Charging, Throwing, () => Charging.isCharged && Charging.AttackState == Throwing);
        AddTransition(Throwing, Charging, () => Throwing.Thrown);
        AddTransition(Blasting, Charging, () => Blasting.Blasted);

        base.StartStateMachine(delay);
    }
}