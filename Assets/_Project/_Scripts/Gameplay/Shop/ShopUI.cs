using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public Shop _shop;
    public GameObject shopPanel;
    private List<PlacedObjectTypeSO> _allItems => _shop.AllItems;
   [SerializeField] private List<ShopButton> _shopButtons = new List<ShopButton>();

   public void DefenderPurchased(ShopButton button)
    {
        _shop.PurchasedItem?.Invoke(button.Item);
    }

    public void CloseShop()
    {
        Shop.ShopToggle?.Invoke(false);
    }

    private void Awake()
    {
        int i = 0;
        for (; i < _shopButtons.Count && i < _allItems.Count; i++)
        {
            _shopButtons[i].Item = _allItems[i];
        }
    }

    public void ToggleShopPanel(bool show) => shopPanel.SetActive(show);
    
}
