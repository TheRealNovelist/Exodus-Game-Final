using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Shop shop;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void DefenderPurchased()
    {
        GUIManager.Instance.ShopPanelOn = false;

    }
}
