using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Attach to BuildingController
public class GroundPlacementController : MonoBehaviour
{
    [SerializeField] private GameObject placeableObjectPrefab;
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private KeyCode newObjectHotKey = KeyCode.F;
    private GameObject currentPlaceableGameObject;
    private float mouseWheelRotation;

    // Update is called once per frame
    void Update()
    {
        HandleNewObjectHotKey();

        if (currentPlaceableGameObject != null)
        {
            MoveCurrentPlaceableObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
        }
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPlaceableGameObject = null;
        }
    }
    private void RotateFromMouseWheel()
    {
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableGameObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    private void MoveCurrentPlaceableObjectToMouse()
    {
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);

         
        if (Physics.Raycast(ray,out RaycastHit hitInfo, 100f))
        {
            currentPlaceableGameObject.transform.position = hitInfo.point;
            currentPlaceableGameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }
    
    private void HandleNewObjectHotKey()
    {
        if (Input.GetKeyDown(newObjectHotKey))
        {
            if (currentPlaceableGameObject == null)
            {
                currentPlaceableGameObject = Instantiate(placeableObjectPrefab);
            }
            else
            {
                Destroy(currentPlaceableGameObject);
            }
        }
    }
}
