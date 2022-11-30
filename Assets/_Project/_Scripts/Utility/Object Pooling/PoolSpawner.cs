using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawner : MonoBehaviour
{
    float spawnTime = 5f;
    float timeToSpawn;
    Pooler pooler;
    GameObject newPrefab;

    // Start is called before the first frame update
    void Start()
    {
        pooler = FindObjectOfType<Pooler>();
    }

    // Update is called once per frame
    void Update()
    {
        timeToSpawn += Time.deltaTime;
        //Debug.Log("Countdown: " + timeToSpawn);
        if (timeToSpawn >= spawnTime)
        {
            newPrefab = pooler.LivePool();
            newPrefab.transform.position = this.transform.position;
        }

        if (timeToSpawn >= 7f)
        {
            pooler.DeadPool(newPrefab);
        }
    }
}
