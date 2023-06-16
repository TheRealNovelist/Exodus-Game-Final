using System;
using System.Collections;
using System.Collections.Generic;
using EnemySystem;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Used in enemy spawner rooms
/// </summary>
public class EnemyRoom : Room
{
    [Header("Lock Conditions")] [SerializeField]
    private List<DoorDoubleSlide> doors;

    [HideInInspector] public bool roomLocked = false;
    public Action LockRoom, UnlockRoom;

    protected override void Awake()
    {
base.Awake();        
        if (doors == null || doors.Count == 0)
        {
            return;
        }

        foreach (var door in doors)
        {
            door.Init(this);
        }
    }

    private void Start()
    {
        roomLocked = false;
    }

    private void OnEnable()
    {
        RespawnPlayer.OnPlayerStartRespawn += EnemySpawner.DisableAllEnemiesInRoom;
        RespawnPlayer.OnPlayerFinishedRespawn += EnemySpawner.Reset;

        LockRoom += LockDoors;
        UnlockRoom += UnlockDoors;
    }

    private void OnDisable()
    {
        LockRoom -= LockDoors;
        UnlockRoom -= UnlockDoors;
        RespawnPlayer.OnPlayerStartRespawn -= EnemySpawner.DisableAllEnemiesInRoom;
        RespawnPlayer.OnPlayerFinishedRespawn -= EnemySpawner.Reset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !EnemySpawner.FinishedSpawning)
        {
            LockRoom?.Invoke();
        }
    }

    private void LockDoors()
    {
        roomLocked = true;
    }

    private void UnlockDoors()
    {
        roomLocked = false;
    }
}