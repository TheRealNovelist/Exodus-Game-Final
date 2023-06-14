using System.Collections;
using System.Collections.Generic;
using EnemySystem;
using UnityEngine;
using System;
using Unity.VisualScripting;


public class EnemySpawnerSystem : BaseAI
{
    [SerializeField] private Transform _player;
    public Transform Player => _player;
    [SerializeField] private bool spawnOnStart;
    
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
    public int Spawned => spawned;
    private int spawned =0;
    private int defeated = 0;
    private Room room;
    
    [SerializeField]  private float waveCountDownTime =10;

    public List<BaseEnemy> EnemiesInRoom = new List<BaseEnemy>();

    public bool FinishedSpawning { get; private set; }

    public Action<BaseEnemy> EnemySpawned, EnemyDefeated;


    protected override void Awake()
    {
        base.Awake();

        if (spawnOnStart) Activate();
    }

    public void Activate()
    {
        if (!IsStateMachineStarted())
        {
            StartStateMachine();
        }
    }

    public override void StartStateMachine(float delay = 0)
    {
        if (IsStateMachineStarted()) return;

        var waitingState = new ES_WaitingState(waveCountDownTime);
        var wavingState = new ES_WavingState(this);
        
        AddTransition(waitingState, wavingState, ()=>waitingState.FinishedCounting);
        AddTransition(wavingState, waitingState, () =>  wavingState.SpawnedEnough());
        
        initialState = waitingState;

        base.StartStateMachine(delay);
    }

    private void Start()
    {
        EnemySpawned += SpawnedEnemy;
        EnemyDefeated += DefeatedEnemy;

        if (room)
        {
            EnemyRoom enemyRoom = room as EnemyRoom;

            if (enemyRoom)
            {
                enemyRoom.LockRoom+= Activate;
            }
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        
        EnemySpawned -= SpawnedEnemy;
        EnemyDefeated -= DefeatedEnemy;
    }

    private void SpawnedEnemy(BaseEnemy enemy)
    {
        spawned++;

        if (spawned >= totalToSpawn)
        {
            _stateMachine.Stop();
            FinishedSpawning = true;
        }
        
        EnemiesInRoom.Add(enemy);
    }

    private void DefeatedEnemy(BaseEnemy enemy)
    {
        defeated++;

        //Defeated all
        if (defeated >= totalToSpawn && room)
        {
            EnemyRoom enemyRoom = room as EnemyRoom;

            if (enemyRoom)
            {
                enemyRoom.UnlockRoom?.Invoke();
            }
        }
        
        EnemiesInRoom.Remove(enemy);

    }

    public void Init(Room room)
    {
        this.room = room;
    }

    public bool IsWaving() => _stateMachine.GetCurrentState() is ES_WavingState;

    public void DisableAllEnemiesInRoom()
    {
        foreach (BaseEnemy enemy in EnemiesInRoom)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    public void EnableAllEnemiesInRoom()
    {
        foreach (BaseEnemy enemy in EnemiesInRoom)
        {
            enemy.gameObject.SetActive(true);
        }
    }

}
