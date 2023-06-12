using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossRoom : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private List<BossEnemy> enemies = new List<BossEnemy>();
    [SerializeField] private List<DoorDoubleSlide> doors = new List<DoorDoubleSlide>();

    public Action OnEnemyDied;
    public Action<bool> OnEnemyActivated;
    public Action OnRoomPassed;

    private int _defeatedEnemies = 0;
    private int _totalEnemies;
    [HideInInspector] public bool Locked = false;
    
    private void Start()
    {
        _defeatedEnemies = 0;
        _totalEnemies = enemies.Count;
        Locked = false;
        
        Debug.Log("start");
   
        foreach (DoorDoubleSlide door in doors)
        {
            door.Init(this);
        }

        OnEnemyActivated?.Invoke(false);
    }

    private void ReloadScene()
    {
        if (Locked)
        {
            Debug.Log("reloafd");
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }

    private void UnLoadScene()
    {
        if (Locked)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                SceneManager.UnloadSceneAsync(sceneName);
            }
        }
    }

    private void OnEnable()
    {
        OnEnemyDied += UpdateDefeated;
        OnEnemyActivated += ToggleAllEnemies;
        OnRoomPassed += UnlockRoom;

        RespawnPlayer.OnPlayerStartRespawn += OffAllEnemies;
        
        RespawnPlayer.OnPlayerStartRespawn += UnLoadScene;
        RespawnPlayer.OnPlayerFinishedRespawn += ReloadScene;
    }

    private void OnDisable()
    {
        OnEnemyDied -= UpdateDefeated;
        OnEnemyActivated -= ToggleAllEnemies;
        OnRoomPassed -= UnlockRoom;
        
        RespawnPlayer.OnPlayerStartRespawn -= OffAllEnemies;
        
        RespawnPlayer.OnPlayerStartRespawn -= UnLoadScene;
        RespawnPlayer.OnPlayerFinishedRespawn -= ReloadScene;
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
        foreach (BossEnemy e in enemies)
        {
            e.Init(this);
        }
    }

    private void ToggleAllEnemies(bool activate)
    {
        foreach (BossEnemy e in enemies)
        {
            e.enabled = activate;
        }
    }
    
    private void OffAllEnemies()
    {
        foreach (BossEnemy e in enemies)
        {
            e.enabled = false;
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