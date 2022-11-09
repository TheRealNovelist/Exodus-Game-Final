using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adapted from Jason Weimann

/// <summary>
/// <i><b>Game Event Scriptable Object: Easy to drag and drop new events and pass around argument when needed.</b></i>
/// <para>
/// <b>Guideline:</b>
/// It is not needed to pass in a parameter, but if needed, please include the following parameter into the invoked method:
/// <code>(object data)</code>
/// Depending on the implementation, you can add a check into the method to see if the type matched, and cast the data type to use them.
/// </para>
/// </summary>
[CreateAssetMenu(menuName = "Utility/Game Event", fileName = "New Game Event")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> _listeners = new List<GameEventListener>();

    /// <summary>
    /// Invoke all the listeners to do jobs.
    /// </summary>
    /// <param name="data">Pass in optional data. If you have multiple data needed to be passed, use a struct.</param>
    public void Invoke(object data = default)
    {
        if (_listeners.Count == 0) return;    
        
        foreach (var globalEventListener in _listeners)
        {
            globalEventListener.OnEventRaised(data);
        }
    }

    public void Register(GameEventListener gameEventListener)
    {
        if (!_listeners.Contains(gameEventListener)) 
            _listeners.Add(gameEventListener);
    }

    public void Deregister(GameEventListener gameEventListener)
    {
        if (_listeners.Contains(gameEventListener)) 
            _listeners.Remove(gameEventListener);
    }
}
