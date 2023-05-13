using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shop : MonoBehaviour
{
    public ShopUI shopUI;
    public  Action<bool> ShopToggle;
    public Action<PlacedObjectTypeSO> PurchasedItem;

    [SerializeField] private GameObject turretCamera;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private AudioManager audioManager;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShopToggle?.Invoke(true);
        }
    }

    private void ToggleCamera(bool cameraTurret)
    {
        turretCamera.SetActive(cameraTurret);
        playerCamera.SetActive(!cameraTurret);
    }

    private void PlaySound(bool toggle)
    {
        if (toggle)
        {
            audioManager.PlayOneShot("TingIns");
        }
        else
        {
            audioManager.PlayOneShot("TingOuts");
        }
    }

    private void Start()
    {
        ShopToggle += ToggleCamera;
        ShopToggle += shopUI.ToggleShopPanel;
        ShopToggle?.Invoke(false);
        PurchasedItem += Purchased;
        ShopToggle += PlaySound;
        PurchasedItem += so => {ShopToggle?.Invoke(false);};
    }

    private void OnDisable()
    {
        ShopToggle -= ToggleCamera;
        ShopToggle -= shopUI.ToggleShopPanel;
    }

    private void Awake()
    {
        shopUI.Init(this);
    }

    private void Purchased(PlacedObjectTypeSO item)
    {
        CoinManager.Instance.SpendCoin(item.price) ; 
    }
    
    
}
