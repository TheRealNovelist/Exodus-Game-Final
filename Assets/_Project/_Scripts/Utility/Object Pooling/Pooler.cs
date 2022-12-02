using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public static Pooler Instance;
    private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
       
    }

    /*public GameObject LivePool()
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
    }*/
    
    /// <summary>
    /// Call when instantiate a game object
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public GameObject GetObject(GameObject obj)
    {
        if(objectPool.TryGetValue(obj.name, out Queue<GameObject> objectList))
        {
            if(objectList.Count == 0)
            {
                return CreateNewObject(obj);
            }
            
            GameObject getObject = objectList.Dequeue();
            getObject.SetActive(true);
            return getObject;
        }

        return CreateNewObject(obj);
    }
    
    private GameObject CreateNewObject(GameObject obj)
    {
        GameObject newObject = Instantiate(obj);
        newObject.name = obj.name;
        return newObject;
    }

    /*public void DeadPool(GameObject newPrefab)
    {
        pool.Enqueue(newPrefab);
        newPrefab.SetActive(false);
    }*/
    
    /// <summary>
    /// Call when destroy game object
    /// </summary>
    /// <param name="obj"></param>
    public void ReturnGameObject(GameObject obj)
    {
        if(objectPool.TryGetValue(obj.name, out Queue<GameObject> objectList))
        {
            objectList.Enqueue(obj);
        }
        else{
            Queue<GameObject> newObjectQueue = new Queue<GameObject>();
            newObjectQueue.Enqueue(obj);
            objectPool.Add(obj.name, newObjectQueue);
        }
        
        obj.SetActive(false);
    }
}
