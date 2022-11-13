using UnityEngine;

public class CursorAndCam : MonoBehaviour
{
    public bool InventoryPanelOn;
    public bool ShopPanelOn;
    
    public static CursorAndCam Instance;

    [SerializeField] private PlayerMove1 playerMovementScr;
    [SerializeField] private PlayerCam playerCamScr;

    [SerializeField] private GameObject turretCamera,playerCamera;

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
        /*if (InventoryPanelOn)
        {
            if (Cursor.lockState != CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            playerMovementScr.recieveInput = false;
            playerCamScr.getPlayerRotation = false;
            return;

        }
        
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerMovementScr.recieveInput = true;
            playerCamScr.getPlayerRotation = true;
            Cursor.visible = false;
        }*/

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
        playerMovementScr.recieveInput = canMove;
        playerCamScr.getPlayerRotation = canMove;
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
        this.playerCamera.SetActive(!turretCamera);
    }
    
    
    
    
}
