using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Room : MonoBehaviour
{
   public EnemySpawnerSystem EnemySpawner;

    public static EnemySpawnerSystem PlayerCurrentAt;


    protected virtual void Start()
    {
        EnemySpawner.Init(this);

        if (PlayerCurrentAt == null)
        {
            var respawn = (RespawnPlayer)FindObjectOfType(typeof(RespawnPlayer));
            PlayerCurrentAt = respawn.StartRoom;
        }
        RespawnPlayer.OnPlayerStartRespawn += PlayerCurrentAt.DisableAllEnemiesInRoom;
        RespawnPlayer.OnPlayerFinishedRespawn += PlayerCurrentAt.EnableAllEnemiesInRoom;
        

    }
}
