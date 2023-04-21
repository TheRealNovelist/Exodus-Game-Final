using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRoomTrigger : MonoBehaviour
{
    [SerializeField] private ESpawnerSystem _spawnerSystem;
    [SerializeField] private MainRoomUI _mainRoomUI;

    public Action<bool> PlayerInMainRoom;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInMainRoom?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInMainRoom?.Invoke(false);
        }
    }

    private void PlayerInRoom(bool inRoom)
    {
        _mainRoomUI.ToggleStats(inRoom);
        _mainRoomUI.ToggleWarning(!inRoom);
    }

    private void Start()
    {
        PlayerInMainRoom += PlayerInRoom;
        
       // PlayerInMainRoom?.Invoke(false);
    }

    private void OnDisable()
    {
        PlayerInMainRoom -= PlayerInRoom;
    }
}
