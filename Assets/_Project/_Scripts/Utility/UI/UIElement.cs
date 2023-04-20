using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour
{
    [SerializeField] private string layer = "";
    
    private void OnEnable()
    {
        UIManager.TryEnableElement(this);
    }

    public string GetLayer()
    {
        return layer;
    }
}
