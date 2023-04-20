using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIManager
{
    private static string currentLayer = "";
    private static List<GameObject> activeUIs;

    public static void TryEnableElement(UIElement element)
    {
        if (currentLayer != element.GetLayer())
        {
            foreach (GameObject ui in activeUIs)
            {
                ui.SetActive(false);
            }
            
            activeUIs.Clear();
            currentLayer = element.GetLayer();
        }
        
        activeUIs.Add(element.gameObject);
    }
}
