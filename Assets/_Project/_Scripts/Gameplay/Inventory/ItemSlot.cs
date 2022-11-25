using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item _item;
    public Image image;
    public int amount = 0;
    
    public TextMeshProUGUI quantityTMP;
    [SerializeField] private Color32 lockColor = new Color32(90, 90, 90,225);
    private Color32 unlockColor = new Color32(255, 255, 255,255);
    private Color32 nullColor = new Color32(0, 0, 0,0);


    public void UpdateSlotUI()
    {
        if (_item == null)
        {
            quantityTMP.text = "";
            image.color = nullColor;
            image.sprite = null;
            return;
        }

        image.sprite = _item.icon;

        switch (_item)
        {
            case EquipItem when amount > 0:
                image.color = unlockColor;
                quantityTMP.text = "";
                break;
            case EquipItem:
                image.color = lockColor;
                quantityTMP.text = "";
                break;
            default:
                quantityTMP.text = amount.ToString();
                image.color = unlockColor;
                break;
        }
    }
}
