using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    LoadingMap
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState CurrentState { get; private set; }
    
    public static event Action<GameState> OnGameStateChange;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        
    }

    private void SetGameState(GameState state)
    {
        switch (state)
        {
            
        }
    }
}
