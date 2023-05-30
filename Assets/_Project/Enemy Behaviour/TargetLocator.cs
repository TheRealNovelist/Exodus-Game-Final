using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    public GameObject Target { get; private set; }

    private void Awake()
    {
        Target = GameObject.FindWithTag("Player");
    }

    public void LookAt(Transform body, Transform target)
    {
        body.LookAt(new Vector3(target.position.x, body.transform.position.y, target.position.z));
    }
}
