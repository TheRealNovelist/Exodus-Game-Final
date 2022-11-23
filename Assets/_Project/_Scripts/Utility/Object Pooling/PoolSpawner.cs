using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawner : MonoBehaviour
{
    float spawnTime = 5f;
    float timeToSpawn;
    Pooler pooler;
    // Start is called before the first frame update
    void Start()
    {
        pooler = FindObjectOfType<Pooler>();
    }

    // Update is called once per frame
    void Update()
    {
        timeToSpawn += Time.deltaTime;
        if (timeToSpawn >= spawnTime)
        {
            GameObject newPrefab = pooler.LivePool();
            newPrefab.transform.position = this.transform.position;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {

        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {

        }

    }
}
