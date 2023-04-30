using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    public EquipSlot[] equipSlots;

    public void Equip(int index,EquipItem item)
    {
        Item addItem = item as Item;
        Inventory.Instance._inventoryUI.HighLightFrame(Inventory.Instance.slotsByItems[addItem],true);
        equipSlots[index]._item = item;
        equipSlots[index].UpdateSlotUI();
        
        Inventory.Instance._inventoryUI.RefreshUI();
    }
    
    public void Unequip(int index)
    {
        Inventory.Instance._inventoryUI.HighLightFrame(Inventory.Instance.slotsByItems[equipSlots[index]._item as Item],false);
        equipSlots[index]._item = null;
        equipSlots[index].UpdateSlotUI();
    }
    
}
