using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shop : MonoBehaviour
{
    public ShopUI shopUI;
    public List<PlacedObjectTypeSO> AllItems = new List<PlacedObjectTypeSO>();
    public Action<bool> ShopToggle;
    public Action<PlacedObjectTypeSO> PurchasedItem;

    [SerializeField] private GameObject turretCamera;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private BackGroundMusic backGroundMusic;//the object that have the script referenced is on player name AudioHole

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShopToggle?.Invoke(true);
        }
    }

    private void Start()
    {
        ShopToggle?.Invoke(false);
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
          if(backGroundMusic){  backGroundMusic.PlayBackBroundMusic(toggle);}
          else{ Debug.LogWarning($"backGroundMusic of {gameObject.name} is missing");}
        }
        else
        {
            if(backGroundMusic) backGroundMusic.PlayBackBroundMusic(toggle);
            audioManager.PlayOneShot("TingOuts");
        }
    }

    private void OnEnable()
    {
        ShopToggle += ToggleCamera;
        ShopToggle += shopUI.ToggleShopPanel;
        ShopToggle += PlaySound;
        ShopToggle += PlayerCursor.ToggleCursor;
        
      //  PurchasedItem += so => {ShopToggle?.Invoke(false);};
    }

    private void OnDisable()
    {
        ShopToggle -= ToggleCamera;
        ShopToggle -= shopUI.ToggleShopPanel;
        //PurchasedItem -= (c)=>{shopUI.canvasUI.SetActive(false);};
        ShopToggle -= PlaySound;
        ShopToggle -= PlayerCursor.ToggleCursor;

    }




    public void CoinPurchased(PlacedObjectTypeSO item)
    {
        if (CoinManager.Instance.SpendCoin(item.price))
        {
            shopUI.shopPanel.SetActive(false);
            PurchasedItem?.Invoke(item);
           // canvasUI.SetActive(false);
        }
    }
    
    
}
