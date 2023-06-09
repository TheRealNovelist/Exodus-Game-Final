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

    private void Awake()
    {
        if (doors == null || doors.Count == 0)
        {
            return;
        }

        foreach (var door in doors)
        {
            door.Init(this);
        }
    }

    protected override void Start()
    {
        base.Start();
        roomLocked = false;

        LockRoom += LockDoors;
        UnlockRoom += UnlockDoors;
    }

    private void OnDisable()
    {
        LockRoom -= LockDoors;
        UnlockRoom -= UnlockDoors;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !EnemySpawner.FinishedSpawning)
        {
            PlayerCurrentAt = EnemySpawner;

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