using UnityEngine;
using System;
using System.Collections.Generic;

public class TurretBuildingSystem : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    
    public static PlacedObjectTypeSO placedObjectTypeSO;
    
    private static Transform visual;
    
    // Start is called before the first frame update
    void Start()
    {
        _shop.PurchasedItem += SetPlaceObject;
    }

    private void OnDisable()
    {
        _shop.PurchasedItem -= SetPlaceObject;
    }

    private void SetPlaceObject(PlacedObjectTypeSO turret)
    {
        placedObjectTypeSO = turret;
    }

    public static void SetPreviewVisual(TurretSlot slot)
    {
        ResetPreview();

        if (placedObjectTypeSO != null) 
        {
            visual = Instantiate(placedObjectTypeSO.visual, slot.gameObject.transform.position, Quaternion.identity);
            visual.parent = slot.transform;
            visual.localPosition = Vector3.zero;
            visual.localEulerAngles = Vector3.zero;
            SetLayerRecursive(visual.gameObject, 7);
        }
        
    }

    public static void ResetPreview()
    {
        if (visual != null) {
            Destroy(visual.gameObject);
            visual = null;
        }
    }

    private static void SetLayerRecursive(GameObject targetGameObject, int layer) {
        targetGameObject.layer = layer;
        foreach (Transform child in targetGameObject.transform) {
            SetLayerRecursive(child.gameObject, layer);
        }
    }

    public static void PlaceTurret(TurretSlot slot)
    {
        if (placedObjectTypeSO != null) 
        {
            var newTurret = Instantiate(placedObjectTypeSO.prefab, slot.gameObject.transform.position, Quaternion.identity);
            newTurret.parent = slot.transform;
            newTurret.localPosition = Vector3.zero;
            newTurret.localEulerAngles = Vector3.zero;
            SetLayerRecursive(newTurret.gameObject, 7);
            
            Shop.ShopToggle?.Invoke(false); 
        }
    }
}
