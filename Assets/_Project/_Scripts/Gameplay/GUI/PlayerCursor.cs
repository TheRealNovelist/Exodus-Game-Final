using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCursor : MonoBehaviour
{

    private Shop _shop;
    private static bool _unlocking = false;

    // Update is called once per frame
    void Update()
    {
        if (_unlocking)
        {
            UnlockCursor();
        }
    }

    private static void UnlockCursor()
    {
        if (Cursor.lockState != CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PlayerInputManager.Input.Disable();
        }
    }

    private static void LockCursor()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            PlayerInputManager.Input.Enable();
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
        ToggleCursor(false);

        _shop = FindObjectOfType<Shop>();

        if (_shop)
            Shop.ShopToggle += ToggleCursor;
    }

    private void OnDisable()
    {
        if (_shop)
            Shop.ShopToggle -= ToggleCursor;
    }
}