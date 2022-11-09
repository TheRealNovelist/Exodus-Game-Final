using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
    public List<EnemySpawner> spawners;
    public float timeBetweenSpawners = 10f;
    private float spawnCountDown = 0;
    private bool counting = true;
    private int spawnersActivated = 0;

    private void Start()
    {
        spawnCountDown = timeBetweenSpawners;
    }

    private void Update()
    {
        if(spawnersActivated > spawners.Count -1) {return;}
        if(!counting){return;}
        
        if (spawnCountDown <= 0)
        {
            ActiveSpawner(spawners[spawnersActivated]);
            spawnersActivated++;
            spawnCountDown = timeBetweenSpawners;
            print(spawnersActivated);
        }
        else
        {
            spawnCountDown -= Time.deltaTime;
        }
    }

    private void ActiveSpawner(EnemySpawner spawner)
    {
        spawner.active = true;
        counting = true;
    }
}
