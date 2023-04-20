using UnityEngine;
using UnityEngine.Serialization;

public class CursorAndCam : MonoBehaviour
{
    public bool InventoryPanelOn;
    public bool ShopPanelOn;
    
    [SerializeField] private GameObject turretCamera;


    // Update is called once per frame
    void Update()
    {
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
        if (Cursor.lockState != CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void MovePlayer(bool canMove)
    {
        // InGameManager.Instance.Player.receiveInput = canMove;
        // InGameManager.Instance.PlayerCamera.getPlayerRotation = canMove;
    }

    public void LockCursor()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void UseTurretCamera(bool turretCamera)
    {
        this.turretCamera.SetActive(turretCamera);
    }
    
}
