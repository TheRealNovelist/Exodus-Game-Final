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

   public void UnlockDoors()
    {

    }
   
   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag("Player"))
      {

        }  
   }
}
