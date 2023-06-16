using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRoom : Room
{
    [SerializeField] private MainRoomUI _mainRoomUI;

    public Action<bool> PlayerInMainRoom;
    private bool _waving;

    [SerializeField] private float startTime = 300f;

    public bool Waving
    {
        get => _waving;
        set
        {
            _waving = value;

            if (value)
            {
                _mainRoomUI.WarningActivate(true);
                _mainRoomUI.UpdateWaveStats(EnemySpawner);
            }
            else
            {
                _mainRoomUI.UpdateWaveStats();
                _mainRoomUI.WarningActivate(false);
            }
        }
    }

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
        PlayerInMainRoom?.Invoke(false);
        StartCoroutine(WaitToActiveSpawner());
    }

    private IEnumerator WaitToActiveSpawner()
    {
        EnemySpawner.gameObject.SetActive(false);

        yield return new WaitForSeconds(startTime);
        
        EnemySpawner.gameObject.SetActive(true);
        EnemySpawner.Activate();
    }

    private void OnEnable()
    {
        PlayerInMainRoom += PlayerInRoom;
        RespawnPlayer.OnPlayerStartRespawn += EnemySpawner.DisableAllEnemiesInRoom;
        RespawnPlayer.OnPlayerFinishedRespawn += EnemySpawner.EnableAllEnemiesInRoom;
    }

    private void OnDisable()
    {
        RespawnPlayer.OnPlayerStartRespawn -= EnemySpawner.DisableAllEnemiesInRoom;
        RespawnPlayer.OnPlayerFinishedRespawn -= EnemySpawner.EnableAllEnemiesInRoom;
        PlayerInMainRoom -= PlayerInRoom;
    }

    private void Update()
    {
        Waving = EnemySpawner.IsWaving();
    }
}