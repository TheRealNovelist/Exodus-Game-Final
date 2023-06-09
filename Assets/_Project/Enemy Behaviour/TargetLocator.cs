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
}
