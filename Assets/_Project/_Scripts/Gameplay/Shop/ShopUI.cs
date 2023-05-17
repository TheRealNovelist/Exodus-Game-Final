using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    private Shop _shop;
    public GameObject shopPanel;

    public void Init(Shop shop)
    {
        _shop = shop;
    }

    public void DefenderPurchased(PlacedObjectTypeSO placedObjectTypeSo)
    {
        _shop.PurchasedItem?.Invoke(placedObjectTypeSo);
    }

    public void CloseShop()
    {
        Shop.ShopToggle?.Invoke(false);
    }
    
    
    public void ToggleShopPanel(bool show) => shopPanel.SetActive(show);
    
}
