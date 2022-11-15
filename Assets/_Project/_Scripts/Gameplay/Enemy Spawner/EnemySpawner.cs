using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnState
{
    Spawning,
    Waiting,
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private WeightedRandomList<GameObject> enemies;
    [SerializeField] private List<Transform> spawners;

    [SerializeField] private float randGap = 5f;
    [SerializeField] private int totalToSpawn = 20;
    private int spawned =0;
    
    private float waveCountDown;
    private SpawnState state = SpawnState.Waiting;
    private bool waiting = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (waveCountDown <= 0)
        {
            //spawn a wave
            
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }

    }
    
}
