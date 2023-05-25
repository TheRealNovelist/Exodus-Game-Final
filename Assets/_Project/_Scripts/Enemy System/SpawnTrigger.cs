using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemySystem;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] private List<BaseEnemy> _baseEnemies;

    [SerializeField] private bool isTrue;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isTrue)
            {
                Debug.Log("Ended overlapping" + other.gameObject.name);
                foreach (BaseEnemy baseEnemy in _baseEnemies)
                {
                    if (!baseEnemy.IsStateMachineStarted())
                    {
                        //Start spawning enemy
                        baseEnemy.target = other.transform;
                        baseEnemy.StartStateMachine();  
                    }
                    else
                    {
                        baseEnemy.Pause(false);
                    }
                }

                isTrue = true;
            }
            else
            {
                foreach (BaseEnemy baseEnemy in _baseEnemies)
                {
                    baseEnemy.Pause(true);
                }
                isTrue = false;
            }
        }
    }
}
