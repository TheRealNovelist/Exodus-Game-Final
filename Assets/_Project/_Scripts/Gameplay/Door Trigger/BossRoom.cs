using System;
using System.Collections.Generic;
using EnemySystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossRoom : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private List<DoorDoubleSlide> doors = new List<DoorDoubleSlide>();

    public Action OnEnemyDied;
    public Action<bool> OnEnemyActivated;
    public Action OnRoomPassed;

    private int _defeatedEnemies = 0;
    private int _totalEnemies;
    private Collider _roomCollider;

    [HideInInspector] public bool Locked = false;

    private void Start()
    {
        _defeatedEnemies = 0;
        _totalEnemies = enemies.Count;
        Locked = false;
        OnEnemyActivated?.Invoke(false);
    }

    private void ResetRoom()
    {
        if (Locked)
        {
            Debug.Log("reloafd");

            ResetAllEnemies();

            Start();
        }
    }

    private void OnEnable()
    {
        OnEnemyDied += UpdateDefeated;
        OnEnemyActivated += ToggleAllEnemies;
        OnRoomPassed += UnlockRoom;
        OnEnemyActivated += (b) => { _roomCollider.enabled = !b; };
        RespawnPlayer.OnPlayerStartRespawn += OffAllEnemies;
        RespawnPlayer.OnPlayerFinishedRespawn += ResetRoom;
    }

    private void OnDisable()
    {
        OnEnemyDied -= UpdateDefeated;
        OnEnemyActivated -= ToggleAllEnemies;
        OnRoomPassed -= UnlockRoom;
        OnEnemyActivated -= (b) => { _roomCollider.enabled = !b; };

        RespawnPlayer.OnPlayerStartRespawn -= OffAllEnemies;
        RespawnPlayer.OnPlayerFinishedRespawn -= ResetRoom;

    }

    private void UnlockRoom() => Locked = false;

    private void UpdateDefeated()
    {
        _defeatedEnemies++;

        if (_defeatedEnemies == _totalEnemies)
        {
            OnRoomPassed?.Invoke();
        }
    }

    private void Awake()
    {
        foreach (GameObject e in enemies)
        {
            if (e.TryGetComponent(out Enemy enemy))
            {
                enemy.Init(this);
            }
            else if (e.TryGetComponent(out Juggernaut juggernaut))
            {
                juggernaut.Init(this);
            }
        }

        foreach (DoorDoubleSlide door in doors)
        {
            door.Init(this);
        }

        _roomCollider = GetComponent<Collider>();
    }

    private void ToggleAllEnemies(bool activate)
    {
        foreach (GameObject e in enemies)
        {
            e.SetActive(activate);
        }
    }

    private void OffAllEnemies()
    {
        foreach (GameObject e in enemies)
        {
            e.SetActive(false);
        }
    }

    private void ResetAllEnemies()
    {
        foreach (GameObject e in enemies)
        {
            e.SetActive(true);

            if (e.TryGetComponent(out Enemy enemy))
            {
                enemy.Reset();
            }
            
            if (e.TryGetComponent(out Juggernaut juggernaut))
            {
                juggernaut.Reset();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnEnemyActivated?.Invoke(true);
            Locked = true;
        }
    }
}