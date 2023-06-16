using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.Serialization;

public class InventoryUI : MonoBehaviour
{
    public List<ItemSlot> itemSlots;

    [Header("Equip Options")]
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private Vector3 offset;
    
    
    [FormerlySerializedAs("notificationTextTMP")]
    [FormerlySerializedAs("notiTMP")]
    [Header("Notification")]
    [SerializeField] private NotificationText notification;
    [SerializeField] private float notiDuration = 2;

    
    [SerializeField] private Sprite highlightFrame,normalFrame;
    
    public EquipmentPanel gunPanel, abilityPanel;


    public void HighLightFrame(ItemSlot slot, bool highLight)
    {
        if (highLight) slot.SetFrameIMG(highlightFrame);
        else slot.SetFrameIMG(normalFrame);
    }
    
    #region Equip Option Panel

    public void EquipItemSlotSelected(bool show = true)
    { 
        var selectedButton = EventSystem.current.currentSelectedGameObject;
        var selectedSlot = selectedButton.GetComponent<ItemSlot>();
        
        var itemOfSelectedSlot = Inventory.Instance.slotsByItems.FirstOrDefault(item => item.Value == selectedSlot).Key;
        EquipItem selectedEquipment = itemOfSelectedSlot as EquipItem;

        if (selectedEquipment != null)
        {
            if (selectedEquipment.unlocked && selectedEquipment.equipping == false)
            {
                optionPanel.gameObject.SetActive(show);
                optionPanel.GetComponent<RectTransform>().position = selectedButton.GetComponent<RectTransform>().position + offset;
                currentSelecting = itemOfSelectedSlot as EquipItem;
            }
            else
            {
                optionPanel.gameObject.SetActive(!show);
            }  
        }
        else
        {
            selectedSlot.UseItem();
            PopUpNoti(itemOfSelectedSlot,false);
        }
       
        
    }


    #endregion

    public void PopUpNoti(Item item, bool addToInventory = true)
    {
        if (addToInventory)
        {
            notification.PopUp($"Added {item} to inventory", notiDuration);
        }
        else
        {
            notification.PopUp($"Added {item.itemName}", notiDuration);
        }
    }
    


    private void Awake()
    {
        optionPanel.SetActive(false);
    }

    public void OptionPanelVisible(bool show) => optionPanel.SetActive(show);

        
    public EquipItem currentSelecting;

    
    public void EquipOption(int index)
    {
        switch (currentSelecting.Type)
        {
            case (EquipType.Ability):
                Inventory.Instance.UnEquip(index,EquipType.Ability);
                Inventory.Instance.Equip(index,currentSelecting);
                break;
            case (EquipType.Gun):
                Inventory.Instance.UnEquip(index,EquipType.Gun);
                Inventory.Instance.Equip(index,currentSelecting);
                break;
        }
        
        OptionPanelVisible(false);
    }
    
    #region Slot UI
    public void RefreshUI(ItemSlot slot)
    {
        slot.UpdateSlotUI();
    }
    
    public void RefreshUI()
    {
        for (int j = 0;j < itemSlots.Count; j++)
        {
            if(itemSlots[j]._item is not EquipItem) itemSlots[j].UpdateSlotUI();
            else itemSlots[j].UpdateSlotUI();
        }
    }
    
    #endregion

}
