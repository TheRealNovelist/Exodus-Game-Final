using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public Shop _shop;
    public GameObject shopPanel;
    
    public GameObject canvasUI;

    private List<PlacedObjectTypeSO> _allItems => _shop.AllItems;
   [SerializeField] private List<ShopButton> _shopButtons = new List<ShopButton>();

   public void DefenderPurchased(ShopButton button)
   {
       _shop.CoinPurchased(button.Item);
   }

    public void CloseShop()
    {
        _shop.ShopToggle?.Invoke(false);
    }

    private void Awake()
    {
        int i = 0;
        for (; i < _shopButtons.Count && i < _allItems.Count; i++)
        {
            _shopButtons[i].Item = _allItems[i];
        }
    }

    private void OnEnable()
    {
        _shop.ShopToggle += (b) =>
        {
            if (b == false)
            {
                canvasUI.SetActive(true);
            }
        };
        
        _shop.PurchasedItem += (c)=>{canvasUI.SetActive(false);};

    }
    
    private void OnDisable()
    {
        _shop.ShopToggle -= (b) =>
        {
            if (b == false)
            {
                canvasUI.SetActive(true);
            }
        };
        
        _shop.PurchasedItem -= (c)=>{canvasUI.SetActive(false);};
    }

    public void ToggleShopPanel(bool show) => shopPanel.SetActive(show);
    
}
