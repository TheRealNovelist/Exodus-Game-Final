using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjectForce : MonoBehaviour, IPoolable
{
    float yForce = 1f;
    float xForce = 1f;
    float zForce = 1f;

    public void OnSpawn()
    {
        float xF = Random.Range(-xForce, xForce);
        float yF = Random.Range(yForce, yForce * 2);
        float zF = Random.Range(-zForce, zForce);

        Vector3 force = new Vector3(xF, yF, zF);
        GetComponent<Rigidbody>().velocity = force;
    }
}
