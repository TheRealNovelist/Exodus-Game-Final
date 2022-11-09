using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private EquipmentPanel gunPanel, abilityPanel;
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject inventoryPanel;


    public EquipItem currentSelecting;
    [SerializeField] private Vector3 offset;


    private void Start()
    {
        inventoryPanel.SetActive(false);
    }

    public void EquipItemSlotSelected(bool show = true)
    {
        var selectedButton = EventSystem.current.currentSelectedGameObject;

        var selectedSlot = selectedButton.GetComponent<ItemSlot>();
        var itemOfSelectedSlot = inventory.slotsByItems.FirstOrDefault(item => item.Value == selectedSlot).Key;
        EquipItem selectedEquipment = itemOfSelectedSlot as EquipItem;

        if (selectedEquipment != null && selectedEquipment.unlocked && selectedEquipment.equipping == false)
        {
            optionPanel.gameObject.SetActive(show);
            optionPanel.GetComponent<RectTransform>().position = selectedButton.GetComponent<RectTransform>().position + offset;
            currentSelecting = itemOfSelectedSlot as EquipItem;
            print("bruh");
        }
        else
        {
            optionPanel.gameObject.SetActive(!show);
        }
    }

    public void EquipOption(int idex)
    {
        switch (currentSelecting.type)
        {
            case (EquipType.Ability):
                abilityPanel.Unequip(idex);
                abilityPanel.Equip(idex,currentSelecting);
                break;
            case (EquipType.Gun):
                gunPanel.Unequip(idex);
                gunPanel.Equip(idex,currentSelecting);
                break;
        }
        
        optionPanel.SetActive(false);
    }
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            inventoryPanel.SetActive(true);
            //_gameEvent.Invoke(true);
            GUIManager.Instance.InventoryPanelOn = true;
        } 
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            inventoryPanel.SetActive(false);
            //_gameEvent.Invoke(false);
            optionPanel.SetActive(false);
            GUIManager.Instance.InventoryPanelOn = false;
        } 
    }
}
