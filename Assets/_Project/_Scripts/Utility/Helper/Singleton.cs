using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject
                {
                    name = typeof(T).Name,
                    //hideFlags = HideFlags.HideAndDontSave
                    
                };
                _instance = obj.AddComponent<T>();
                Debug.Log("I got spawned in!");
            }

            return _instance;
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}

public abstract class SingletonPersistent<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance => _instance;

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}

