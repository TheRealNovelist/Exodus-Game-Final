using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPoolSpawner : MonoBehaviour
{
    Pooler pooler;
    // Start is called before the first frame update
    void Start()
    {
        pooler = Pooler.callInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            pooler.SpawnFromPool("Cube", transform.position, Quaternion.identity);

        }

    }
}
