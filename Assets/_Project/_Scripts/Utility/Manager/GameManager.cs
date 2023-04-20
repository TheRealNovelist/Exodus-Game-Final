using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Loading,
    Running,
    Paused,
}

public class GameManager : Singleton<GameManager>
{
    private GameState _currentState;
    
    public static event Action<GameState> OnGameStateChange;
    
    public void SetGameState(GameState state)
    {
        if (state == _currentState) return;
        
        _currentState = state;
        
        OnGameStateChange?.Invoke(state);
    }
}
