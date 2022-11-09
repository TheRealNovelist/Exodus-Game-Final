using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public Dictionary<string, Queue<GameObject>> poolDict;
    public List<Pool> pools;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int amount;
    }

    public static Pooler callInstance;

    private void Awake()
    {
        callInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        poolDict = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.amount; i++)
            {
                GameObject gameObject = Instantiate(pool.prefab);
                gameObject.SetActive(false);
                objectPool.Enqueue(gameObject);
            }
            poolDict.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rot)
    {
        if (!poolDict.ContainsKey(tag))
        {
            Debug.Log("Pool with tag " + tag + " does not exist.");
            return null;
        }

        GameObject spawnableObj = poolDict[tag].Dequeue();

        spawnableObj.SetActive(true);
        spawnableObj.transform.position = pos;
        spawnableObj.transform.rotation = rot;

        IPoolable poolable = spawnableObj.GetComponent<IPoolable>();

        if (poolable != null)
        {
            poolable.OnSpawn();
        }

        poolDict[tag].Enqueue(spawnableObj);

        return spawnableObj;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
