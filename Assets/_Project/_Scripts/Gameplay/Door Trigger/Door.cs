using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Normal doors open when player enter trigger and will not close afterwards
/// Doors in enemy rooms are locked when player enters the enemyRoom
/// and only opens when all enemies are defeated
/// </summary>
public class Door : MonoBehaviour
{
   private bool Locked
   {
      get
      {
         if (_enemyRoom)
         {
            return _enemyRoom.roomLocked;
         }

         return false;
      }
   }
   
    private bool _playerIn = false;
    private EnemyRoom _enemyRoom;

   private void OnTriggerEnter(Collider other)
   {
      if (!Locked)
      {
         if (other.gameObject.CompareTag("Player"))
         {
            OpenDoor();
         }
      }
   }

   public void Init(EnemyRoom enemyRoom)
   {
      _enemyRoom = enemyRoom;
   }

   public void OpenDoor()
   {
      GetComponent<MeshRenderer>().enabled = false;
   }
   
   public void CloseDoor()
   {
      GetComponent<MeshRenderer>().enabled = true;

   }
}
