using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Adapted from Jason Weimann, with configuration from This is GameDev

/// <summary>
/// Create a custom event to pass in argument. By default, parameter is optional (stated in GameEvent SO)
/// </summary>
[System.Serializable] public class CustomEvent : UnityEvent<object> {}

/// <summary>
/// Add an abstract listener that can listen for an event fired off.
/// </summary>
public class GameEventListener : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private GameEvent _gameEvent;
    [SerializeField] private bool stopListeningOnDisable;
    [Space]
    [SerializeField] private CustomEvent _response;
    
    private bool isEnabled = true;
    
    void Awake() => _gameEvent.Register(this);

    void OnDestroy() => _gameEvent.Deregister(this);

    void OnEnable() => isEnabled = true;

    void OnDisable()
    {
        if (stopListeningOnDisable)
            isEnabled = false;
    }

    public void OnEventRaised(object data)
    {
        if (stopListeningOnDisable && !isEnabled)
            return;
        
        _response.Invoke(data);
    }
}
