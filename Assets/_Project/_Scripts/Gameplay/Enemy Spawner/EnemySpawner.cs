using System;
using System.Collections;
using System.Collections.Generic;
using EnemySystem;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SpawnState
{
    Spawning,
    Waiting,
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    [SerializeField] private WeightedRandomList<BaseEnemy> enemies;
    [SerializeField] private List<Transform> spawners;

    [SerializeField] private float randGap = 5f;
    [SerializeField] private int totalToSpawn = 20;
    
    private int spawned =0;
    private int defeated = 0;
    
    private float waveCountDown;
    private bool waiting = false;
    public bool canSpawn = true;

    private void Start()
    {
        waveCountDown = randGap + Random.Range(0f,5f);
    }

    void Update()
    {
        if(!canSpawn){return;}
        if(spawned>=totalToSpawn){return;}
        if(waiting){return;}
        if (waveCountDown <= 0)
        {
            //spawn a wave
            SpawnEnemy();
            waveCountDown = randGap + Random.Range(0f,5f);
            spawned++;
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }

    }

    private void SpawnEnemy()
    {
        GameObject enemyToSpawn = enemies.GetRandom().gameObject;
        Transform randSpawner = spawners[Random.Range(0, spawners.Count)];
        
        GameObject enemyObj =  Instantiate(enemyToSpawn, randSpawner.position,quaternion.identity);
        
        BaseEnemy enemy = enemyObj.GetComponent<BaseEnemy>();
        enemy.target = player.transform;
        enemy.StartStateMachine();
        
        waiting = false;


        //BaseEnemy enemy = enemyToSpawn.GetComponent<BaseEnemy>();
        //set player as enemy.target here
        //enemy.StartStateMachine
    }

    public void EnemyDie()
    {
        defeated++;
        if (defeated == totalToSpawn)
        {
            //unlock room
        }
    }
    
}
