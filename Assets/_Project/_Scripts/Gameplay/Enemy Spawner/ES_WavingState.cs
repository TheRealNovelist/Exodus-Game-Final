using System.Collections;
using System.Collections.Generic;
using EnemySystem;
using UnityEngine;

public class ES_WavingState : IState
{
    private WeightedRandomList<BaseEnemy> _enemies;
    private List<Transform> _spawners = new List<Transform>();
    private float _gap;
    private int _totalInWave;
    private float currentTimer;

    private int _spawned = 0;

    private EnemySpawnerSystem _spawnerSystem;

    private bool countingdown = true;
    
    public ES_WavingState(EnemySpawnerSystem spawnerSystem)
    {
        _totalInWave = spawnerSystem.AmountInWave;
        currentTimer = _gap = spawnerSystem.SpawnGap;
        _enemies = spawnerSystem.Enemies;
        _spawners = spawnerSystem.Spawners;
        _spawnerSystem = spawnerSystem;
    }
    
    // Update is called once per frame
    public void Update()
    {
        if(!countingdown) {return;}
        if (currentTimer <= 0)
        {
            SpawnEnemy();
            currentTimer = _gap + Random.Range(0f,2f);
        }
        else
        {
            currentTimer -= Time.deltaTime;
        }

    }

    public bool SpawnedEnough() => _spawned == _totalInWave;

    public void OnEnter()
    {
        _spawned = 0;
        currentTimer = _gap ;
        countingdown = true;
        RespawnPlayer.OnPlayerStartRespawn += (() => { countingdown = false;});
        RespawnPlayer.OnPlayerFinishedRespawn += (() => { countingdown = true;});
    }

    public void OnExit()
    {
        RespawnPlayer.OnPlayerStartRespawn -= (() => { countingdown = false;});
        RespawnPlayer.OnPlayerFinishedRespawn -= (() => { countingdown = true;});
    }
    
    private void SpawnEnemy()
    {
        BaseEnemy enemyToSpawn = _enemies.GetRandom();
        Transform randSpawner = _spawners[Random.Range(0, _spawners.Count)];
        
        BaseEnemy enemyObj =  GameObject.Instantiate(enemyToSpawn, randSpawner.position,Quaternion.identity);
        
        enemyObj.StartStateMachine();
        enemyObj.BindSpawner(_spawnerSystem);

        _spawnerSystem.EnemySpawned?.Invoke(enemyObj);
        _spawned++;
    }

}
