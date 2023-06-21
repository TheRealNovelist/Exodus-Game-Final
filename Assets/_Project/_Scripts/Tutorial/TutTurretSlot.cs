using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutTurretSlot : TurretSlot
{
   public override void OnPointerClick(PointerEventData eventData)
   {
      base.OnPointerClick(eventData);

      TutPlacedTurret.OnPlaceTurret?.Invoke();
   }

}
