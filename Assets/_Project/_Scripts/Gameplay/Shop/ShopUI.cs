using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    private Shop _shop;
    [SerializeField] private GameObject shopPanel;

    public void Init(Shop shop)
    {
        _shop = shop;
    }

    public void DefenderPurchased()
    {
        shopPanel.SetActive(false);
    }

    public void CloseShop()
    {
        _shop.ShopToggle?.Invoke(false);
    }
    
    
    public void ToggleShopPanel(bool show) => shopPanel.SetActive(show);
    
}
