using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public bool InventoryPanelOn;
    public bool ShopPanelOn;
    public static GUIManager Instance;

    [SerializeField] private PlayerMove1 playerMovementScr;
    [SerializeField] private PlayerCam playerCamScr;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (InventoryPanelOn || ShopPanelOn)
        {
            if (Cursor.lockState != CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.None;
            }

            playerMovementScr.recieveInput = false;
            playerCamScr.getPlayerRotation = false;
            Cursor.visible = true;
            
            return;
        }
        
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerMovementScr.recieveInput = true;
            playerCamScr.getPlayerRotation = true;
            Cursor.visible = false;
        }
    }
}
