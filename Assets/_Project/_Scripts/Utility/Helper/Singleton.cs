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
                Debug.Log($"  {typeof(T).Name} got spawned in!");
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

    public virtual void Awake()
    {
        T[] tComponents = FindObjectsOfType<T>();
        if (tComponents.Length >= 1)
        {
            _instance = tComponents[0];

            for (int i = 1; i < tComponents.Length; i++)
            {
                Destroy(tComponents[i].gameObject);
            }
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