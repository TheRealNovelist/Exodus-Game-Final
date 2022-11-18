using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveEnemySpawner : MonoBehaviour
{
    public float timeBetweenWaves = 5f;
    public int firstSize = 5;
    public int sizeDifference = 2;
    public List<GameObject> enemyTypes;
    public float spawnRate = 5f;
    
    private float waveCountDown;
    private SpawnState state = SpawnState.Waiting;
    private int spawnedTimes;
    private bool waiting = true;
    public bool active = false;
    

    // Start is called before the first frame update
    void Start()
    {
        waveCountDown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if(!active){return;}
        if (waveCountDown <= 0)
        {
            //spawn a wave
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(WaitToSpawnWave());
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }

    }

    IEnumerator WaitToSpawnWave()
    {
        state = SpawnState.Spawning;
        waveCountDown = timeBetweenWaves;

        //spawn
        for (int i = 0; i < GetQuantityToSpawn(); i++)
        {
            SpawnEnemy(GetEnemyToSpawn(enemyTypes));
            yield return new WaitForSeconds(1 / spawnRate);
        }
        
        spawnedTimes++;

        state = SpawnState.Waiting;
    }

    private int GetQuantityToSpawn()
    {
        int quant = 0;
        quant = firstSize + spawnedTimes * sizeDifference;
        return quant;
    }

    /// <summary>
    /// Return a random enemy from list
    /// Can make this a loot table
    /// </summary>
    private  GameObject GetEnemyToSpawn(List<GameObject> enemies)
    {
        int rand = Random.Range(0, enemies.Count);
        return enemies[rand];
    }

    private void SpawnEnemy(GameObject enemyToSpawn)
    {
        GameObject newEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        
        //TEST
        newEnemy.GetComponent<TurretProjectile>().SetDirect(new Vector3(0,3,0));
    }
}
