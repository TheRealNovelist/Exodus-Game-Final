using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
   private void OnEnable()
   {
      PlayerCursor.ToggleCursor(true);
      PlayerInputManager.Input.Disable();
   }
   private void OnDisable()
   {
      PlayerCursor.ToggleCursor(false);
      PlayerInputManager.Input.Enable();

   }
   
}
