using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    public EquipSlot[] equipSlots;
    public EquipItem[] equippedItems = new EquipItem[2];
    public void Equip(EquipSlot slot,EquipItem item)
    {
        slot._item = item;
        
        int index = Array.IndexOf(equipSlots, slot);
        equippedItems[index] = item;

        item.equipping = true;
        
        slot.UpdateSlotUI();
    }

    public bool HasEmptySlot()
    {
        foreach (var slot in equipSlots)
        {
            if (slot._item == null)
            {
                return true;
            }
        }
        return false;
    }

    public int GetEmptySlot()
    {
        for (int i = 0; i < equipSlots.Length; i++)
        {
            if ( equipSlots[i]._item == null)
            {
                return i;
            }
        }
        return 0;
    }
    
    public void Unequip(EquipSlot slot,EquipItem item)
    {
        slot._item = null;
        
        int index = Array.IndexOf(equipSlots, slot);
        equippedItems[index] = null;

        item.equipping = false;
        
        slot.UpdateSlotUI();
    }
    
    public void Equip(int index,EquipItem item)
    {
        equipSlots[index]._item = item;
        equippedItems[index] = item;

        item.equipping = true;
        
        equipSlots[index].UpdateSlotUI();
    }
    
    public void Unequip(int index)
    {
        equipSlots[index]._item.equipping = false;
        
        equipSlots[index]._item = null;
        equippedItems[index] = null;

        equipSlots[index].UpdateSlotUI();
    }
    
}
