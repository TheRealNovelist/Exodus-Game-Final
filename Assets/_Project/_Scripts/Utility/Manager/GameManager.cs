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

public static class GameManager
{
    private static GameState _currentState;
    
    public static event Action<GameState> OnGameStateChange;
    
    public static void SetGameState(GameState state)
    {
        if (state == _currentState) return;
        
        _currentState = state;
        
        OnGameStateChange?.Invoke(state);
    }
}
