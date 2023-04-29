using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCursor : MonoBehaviour
{
    public bool InventoryPanelOn;
    public bool ShopPanelOn;

    private Shop _shop;
    private static bool _unlocking = false;

    // Update is called once per frame
    void Update()
    {
        if (_unlocking)
        {
            UnlockCursor();
        }
        
        /*if (InventoryPanelOn || ShopPanelOn)
        {
            MovePlayer(false);
        }
        else
        {
            MovePlayer(true);
        }*/
    }

    private static void UnlockCursor()
    {
        if (Cursor.lockState != CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private static void LockCursor()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public static void ToggleCursor(bool unlock)
    {
        if (unlock)
        {
            UnlockCursor();
            _unlocking = true;
        }
        else
        {
            LockCursor();
            _unlocking = false;
        }
    }

    private void Start()
    {
        _shop = FindObjectOfType<Shop>();

        if (_shop)
            _shop.ShopToggle += ToggleCursor;
    }

    private void OnDisable()
    {
        if (_shop)
            _shop.ShopToggle -= ToggleCursor;
    }
}