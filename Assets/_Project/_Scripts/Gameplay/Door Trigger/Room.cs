using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Room : MonoBehaviour
{
    public EnemySpawnerSystem EnemySpawner;


    protected virtual void Awake()
    {
        EnemySpawner.Init(this);

    }
    
}