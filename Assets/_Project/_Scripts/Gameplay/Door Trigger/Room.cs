using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Used in enemy spawner rooms
/// </summary>
public class Room : MonoBehaviour
{
    [Header("Lock Conditions")]
   [SerializeField] private List<DoorDoubleSlide> doors;
   public ESpawnerSystem enemySpawner;
   [HideInInspector] public bool roomLocked = false;
   public Action LockRoom,UnlockRoom;

   [Header("Show Rooms")] [SerializeField]
   private List<GameObject> enableRooms;
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

   private void Start()
   {
       enemySpawner.Init(this);

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
      if (other.gameObject.CompareTag("Player") && !enemySpawner.FinishedSpawning)
      {
          LockRoom?.Invoke();
          Debug.Log(gameObject.name);
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
