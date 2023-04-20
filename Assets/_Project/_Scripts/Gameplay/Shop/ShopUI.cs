using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Shop shop;
    [SerializeField] private GameObject shopPanel;

    // Start is called before the first frame update
    void Start()
    {
        shopPanel.SetActive(false);
    }

    public void DefenderPurchased()
    {
        // InGameManager.Instance.CursorAndCam.ShopPanelOn = false;
        // InGameManager.Instance.CursorAndCam.UseTurretCamera(true);
        shopPanel.SetActive(false);
    }
    
    public void CloseShop()
    {
        shopPanel.SetActive(false);
        // InGameManager.Instance.CursorAndCam.ShopPanelOn = false;
        // InGameManager.Instance.CursorAndCam.LockCursor();
        // InGameManager.Instance.CursorAndCam.UseTurretCamera(false);
    }
    
    public void OnShop()
    {
        shopPanel.SetActive(true);
        // InGameManager.Instance.CursorAndCam.ShopPanelOn = true;
        // InGameManager.Instance.CursorAndCam.UnlockCursor();
    }
    
}
