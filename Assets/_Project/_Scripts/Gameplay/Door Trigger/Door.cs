using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
   private bool Locked
   {
      get
      {
         if (_room)
         {
            return _room.roomLocked;
         }

         return false;
      }
   }
   
    private bool _playerIn = false;
    private Room _room;

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

   public void Init(Room room)
   {
      _room = room;
   }

   public void OpenDoor()
   {
      GetComponent<MeshRenderer>().material.color = Color.cyan;
   }
   
   public void CloseDoor()
   {
      GetComponent<MeshRenderer>().material.color = Color.red;
   }
}
