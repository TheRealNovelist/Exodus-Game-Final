using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBlasterSlot : MonoBehaviour
{
   private SmallBlaster _blaster;
   public void Init(SmallBlaster blaster)
   {
      _blaster = blaster;
   }

   public void ClearSlot()
   {
      _blaster = null;
   }

   public bool SlotAvailable => _blaster == null;
}
