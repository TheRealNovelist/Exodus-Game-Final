using UnityEngine;
using UnityEngine.EventSystems;

public class TurretSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
   public PlacedObjectTypeSO _holdingTurret;
   public bool CanPlace => _holdingTurret == null;

   public void OnPointerEnter(PointerEventData eventData)
   {
      if(!CanPlace) {return;}
      Debug.Log("eneters");
      //Show preview
      TurretBuildingSystem.SetPreviewVisual(this);
   }

   public void OnPointerExit(PointerEventData eventData)
   {
      if(!CanPlace) {return;}
      //Hide preview
      TurretBuildingSystem.ResetPreview();
   }

   public void OnPointerClick(PointerEventData eventData)
   {
      if(!CanPlace) {return;}
      //Hide preview
      TurretBuildingSystem.ResetPreview();

      //Place gameobject
      TurretBuildingSystem.PlaceTurret(this);
   }
}
