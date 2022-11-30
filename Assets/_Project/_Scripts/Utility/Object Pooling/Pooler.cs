using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    Queue<GameObject> pool = new Queue<GameObject>();
    [SerializeField] int poolSize = 2;

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newPrefab = Instantiate(prefab);
            pool.Enqueue(newPrefab);
            newPrefab.SetActive(false);
        }
    }

    public GameObject LivePool()
    {
        if (pool.Count > 0)
        {
            GameObject newPrefab = pool.Dequeue();
            newPrefab.SetActive(true);
            return newPrefab;
        }
        else
        {
            GameObject newPrefab = Instantiate(prefab);
            return newPrefab;
        }
    }

    public void DeadPool(GameObject newPrefab)
    {
        pool.Enqueue(newPrefab);
        newPrefab.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
