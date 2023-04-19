using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
   public bool locked = false;
    private bool _playerIn = false;

   private void OnTriggerEnter(Collider other)
   {
      if (!locked)
      {
         if (other.gameObject.CompareTag("Player"))
         {
            //open door

         }
      }
     

   }

   private void OpenDoor()
   {
      
   }
   
   private void CloseDoor()
   {
      
   }

   public void LockDoor()
   {
      CloseDoor();
      locked = true;
   }
}
