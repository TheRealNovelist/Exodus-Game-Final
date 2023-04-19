using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
   [SerializeField] private List<Door> doors;

   public ESpawnerSystem enemySpawner;
   public Material openMat, closeMat, lockMat;

   [HideInInspector] public bool roomLocked = false;
   public Action LockRoom,UnlockRoom;
   private void Awake()
   {
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
      if (other.gameObject.CompareTag("Player") && !enemySpawner.FinishedSpawnning)
      {
          LockRoom?.Invoke();
      }  
   }

   private void LockDoors()
   {
       roomLocked = true;

       foreach (var door in doors)
       {
           door.CloseDoor();
       }
   }

   private void UnlockDoors()
   {
       roomLocked = false;
   }


}
