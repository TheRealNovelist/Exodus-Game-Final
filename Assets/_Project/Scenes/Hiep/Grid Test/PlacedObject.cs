using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Attach to placedObject(turret, building)
public class PlacedObject : MonoBehaviour
{
    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, PlacedObjectTypeSO.Dir dir,
        PlacedObjectTypeSO placedObjectTypeSO)
    {
        Transform placedObjectTransform = Instantiate(placedObjectTypeSO.prefab.gameObject.transform, worldPosition,
            Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0));
        
        PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();
        placedObject.Setup(placedObjectTypeSO, origin, dir);
        
        return placedObject;
    }
    
    private void Setup(PlacedObjectTypeSO placedObjectTypeSO, Vector2Int origin, PlacedObjectTypeSO.Dir dir)
    {
        this.placedObjectTypeSORe = placedObjectTypeSO;
        this.origin = origin;
        this.dir = dir;
    }
    
    private PlacedObjectTypeSO placedObjectTypeSORe;
    private Vector2Int origin;
    private PlacedObjectTypeSO.Dir dir;

    public List<Vector2Int> GetGridPositionList()
    {
        return placedObjectTypeSORe.GetGridPositionList(origin, dir);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
