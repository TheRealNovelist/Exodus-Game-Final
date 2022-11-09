using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        GUIManager.Instance.ShopPanelOn = false;
    }
    
    public void OnShop()
    {
        shopPanel.SetActive(true);
        GUIManager.Instance.ShopPanelOn = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnShop();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
