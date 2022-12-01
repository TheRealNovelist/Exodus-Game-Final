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
        CursorAndCam.Instance.ShopPanelOn = false;
        CursorAndCam.Instance.UseTurretCamera(true);
    }
    
    public void CloseShop()
    {
        shopPanel.SetActive(false);
        CursorAndCam.Instance.ShopPanelOn = false;
        CursorAndCam.Instance.LockCursor();
        CursorAndCam.Instance.UseTurretCamera(false);
    }
    
    public void OnShop()
    {
        shopPanel.SetActive(true);
        CursorAndCam.Instance.ShopPanelOn = true;
        CursorAndCam.Instance.UnlockCursor();
    }
}
