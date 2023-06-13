using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
   public TextMeshProUGUI statusText;

   private void OnEnable()
   {
     PlayerCursor.ToggleCursor(true);
   }
}
