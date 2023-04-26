using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Cursor : MonoBehaviour
{
    public bool InventoryPanelOn;
    public bool ShopPanelOn;

    private Shop _shop;
    private bool _unlocking = false;

    // Update is called once per frame
    void Update()
    {
        if (_unlocking)
        {
            UnlockCursor();
        }

        if (InventoryPanelOn || ShopPanelOn)
        {
            MovePlayer(false);
        }
        else
        {
            MovePlayer(true);
        }
    }

    public void UnlockCursor()
    {
        if (UnityEngine.Cursor.lockState != CursorLockMode.None)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
    }

    public void MovePlayer(bool canMove)
    {
        // InGameManager.Instance.Player.receiveInput = canMove;
        // InGameManager.Instance.PlayerCamera.getPlayerRotation = canMove;
    }

    public void LockCursor()
    {
        if (UnityEngine.Cursor.lockState != CursorLockMode.Locked)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }
    }

    private void ToggleCursor(bool unlock)
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
        _shop = FindObjectOfType(typeof(Shop)).GetComponent<Shop>();

        if (_shop)
            _shop.ShopToggle += ToggleCursor;
    }

    private void OnDisable()
    {
        if (_shop)
            _shop.ShopToggle -= ToggleCursor;
    }
}