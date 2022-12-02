using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawner : MonoBehaviour
{
    float spawnTime = 1f;
    float timeToSpawn;
    public GameObject newPrefab;

    private void Start()
    {
        InvokeRepeating("TestSpawn",0,0.3f);
    }

    private void TestSpawn()
    {
        GameObject newObj = Pooler.Instance.GetObject(newPrefab);
        newObj.transform.position = this.transform.position;
        StartCoroutine(WaitToDestroy(newObj));
    }


    IEnumerator WaitToDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        Pooler.Instance.ReturnGameObject(obj);
    }
}
