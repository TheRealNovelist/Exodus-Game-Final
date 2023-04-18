using System.Collections;
using System.Collections.Generic;
using EnemySystem;
using UnityEngine;
using System;
using Unity.VisualScripting;


public class ESpawnerSystem : BaseAI
{
    [SerializeField] private WeightedRandomList<BaseEnemy> enemies;
    [SerializeField] private List<Transform> spawners;
    [SerializeField] private int totalToSpawn = 20;
    [SerializeField] private int amountInWave = 4;
    [SerializeField] private float spawnGap = 5;
    public WeightedRandomList<BaseEnemy> Enemies => enemies;
    public List<Transform> Spawners => spawners;
    public int TotalToSpawn => totalToSpawn;
    public int AmountInWave => amountInWave;
    public float SpawnGap=> spawnGap;
    
    private int spawned =0;
    private int defeated = 0;
    
    [SerializeField]  private float waveCountDownTime =10;

    public Action EnemySpawned, EnemyDefeated;

    protected override void Awake()
    {
        base.Awake();
        StartStateMachine();
    }

    public override void StartStateMachine(float delay = 0)
    {
        if (IsStateMachineStarted()) return;

        var waitingState = new ES_WatingState(waveCountDownTime);
        var wavingState = new ES_WavingState(this);
        
        AddTransition(waitingState, wavingState, ()=>waitingState.FinishedCounting);
        AddTransition(wavingState, waitingState, () =>  wavingState.SpawnedEnough());
        
        initialState = waitingState;

        base.StartStateMachine(delay);
    }

    public bool IsStateMachineStarted => _stateMachine.isStarted;

    private void Start()
    {
        EnemySpawned += () =>
        {
            spawned++;
            
            if (spawned >= totalToSpawn)
            {
                Debug.Log($"Spawned {spawned}/{totalToSpawn}");
                _stateMachine.Stop();
            }
        };
    }
}
