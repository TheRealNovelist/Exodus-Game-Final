using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawner : MonoBehaviour
{
    float spawnTime = 5f;
    float timeToSpawn;
    GameObject newPrefab;


    // Update is called once per frame
    void Update()
    {
        timeToSpawn += Time.deltaTime;
        //Debug.Log("Countdown: " + timeToSpawn);
        if (timeToSpawn >= spawnTime)
        {
            //newPrefab = Pooler.Instance.LivePool();
           GameObject  newObj = Pooler.Instance.GetObject(newPrefab);
           newObj.transform.position = this.transform.position;
        }

        if (timeToSpawn >= 7f)
        {
            Pooler.Instance.DeadPool(newPrefab);
        }
    }
}
