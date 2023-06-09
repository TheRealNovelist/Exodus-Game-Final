using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class RoomVisibility : MonoBehaviour
{
    [SerializeField] private List<GameObject> connectedGO = new List<GameObject>();
    private static List<GameObject> previousConnection = new List<GameObject>();

    [SerializeField] private bool disableOnStart;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (previousConnection.Count > 0)
            {
                var roomsToDisable = previousConnection.Except(connectedGO);

                foreach (GameObject room in roomsToDisable)
                {
                    if (room != gameObject)
                    {
                        room.SetActive(false);
                    }
                }
            }

            foreach (GameObject room in connectedGO)
            {
                room.SetActive(true);
            }

            previousConnection = connectedGO;
        }
    }

    private void Start()
    {
        if (disableOnStart)
        {
            foreach (GameObject c in connectedGO)
            {
                if (!c.TryGetComponent(out RoomVisibility room))
                {
                    if (c.activeSelf)
                    {
                        c.SetActive(false);
                    }
                }
            }
            
            gameObject.SetActive(false);
        }
    }
}