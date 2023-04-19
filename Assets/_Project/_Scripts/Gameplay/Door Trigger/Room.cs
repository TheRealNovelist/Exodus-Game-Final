using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
   [SerializeField] private List<Door> doors;
   
   public bool enemyRoom = false;
   
   public ESpawnerSystem enemySpawner;
   
   public Material openMat, closeMat, lockMat;

   public void LockDoors()
   {
      foreach (var door in doors)
      {
         door.LockDoor();
      }
   }
   
   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         if (enemyRoom)
         {
            //lock doors

            gameObject.GetComponent<Collider>().enabled = false;
         }
            
      }
   }
}
