using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.Serialization;


public class Inventory : Singleton<Inventory>
{
    [Header("Items and Slots")] [SerializeField]
    private List<Item> allItemInInventory;

    public InventoryUI _inventoryUI;

    public Dictionary<Item, ItemSlot> slotsByItems = new Dictionary<Item, ItemSlot>();

    [Header("Equip Panels")] public GameEvent equipAbilityEvent;
    public GameEvent equipGunEvent;

    [FormerlySerializedAs("equippedItems")] public EquipItem[] equippedGuns = new EquipItem[2];
    [FormerlySerializedAs("equippedItems")] public EquipItem[] equippedAbilities = new EquipItem[2];


    private void Start()
    {
        SetSlotItem();
        _inventoryUI.RefreshUI();
        _inventoryUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _inventoryUI.gameObject.SetActive(true);
            PlayerCursor.ToggleCursor(true);
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            _inventoryUI.gameObject.SetActive(false);
            _inventoryUI.OptionPanelVisible(false);
            PlayerCursor.ToggleCursor(false);
        }
    }

    #region Inventory

    public void SetSlotItem()
    {
        int j = 0;
        for (; j < allItemInInventory.Count && j < _inventoryUI.itemSlots.Count; j++)
        {
            EquipItem e = allItemInInventory[j] as EquipItem;

            if (e)
            {
                e.equipping = false;
                e.unlocked = false;
            }

            _inventoryUI.itemSlots[j]._item = allItemInInventory[j];
            slotsByItems.Add(allItemInInventory[j], _inventoryUI.itemSlots[j]);
        }

        for (; j < _inventoryUI.itemSlots.Count; j++)
        {
            _inventoryUI.itemSlots[j]._item = null;
        }
    }

    /// <summary>
    /// Called when add item from chest
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(Item item)
    {
        var equipItem = item as EquipItem;
        if (equipItem is not null && AddEquipItem(equipItem))
        {
            // HighLightFrame(slotsByItems[item],true);
        }

        AddToInventory(item);
        _inventoryUI.PopUpNoti(item);
    }

    private void AddToInventory(Item item)
    {
        slotsByItems[item].amount++;
        _inventoryUI.RefreshUI(slotsByItems[item]);
    }

    /// <summary>
    /// Auto equip
    /// </summary>
    /// <param name="equipItem"></param>
    /// <returns></returns>
    private bool AddEquipItem(EquipItem equipItem)
    {
        equipItem.unlocked = true;

        switch (equipItem.type)
        {
            case (EquipType.Ability):
                if (HasEmptySlot(equippedAbilities))
                {
                    int emptySlotIndex = GetEmptySlot(equippedAbilities);
                    Equip(emptySlotIndex, equipItem);
                    return true;
                }

                break;
            case (EquipType.Gun):
                if (HasEmptySlot(equippedGuns))
                {
                    int emptySlotIndex = GetEmptySlot(equippedGuns);
                    Equip(emptySlotIndex, equipItem);
                    return true;
                }

                break;
        }

        return false;
    }

    public void Equip(int index, EquipItem item)
    {
        item.equipping = true;

        switch (item.type)
        {
            case (EquipType.Ability):
                equippedAbilities[index] = item;
                _inventoryUI.abilityPanel.Equip(index, item);
                equipAbilityEvent.Invoke();
                break;

            case (EquipType.Gun):
                equippedGuns[index] = item;
                _inventoryUI.gunPanel.Equip(index, item);

                equipGunEvent.Invoke();
                break;
        }
    }

    public void UnEquip(int index, EquipType type)
    {
        switch (type)
        {
            case (EquipType.Ability):
                equippedAbilities[index].equipping = false;
                equippedAbilities[index] = null;
                _inventoryUI.abilityPanel.Unequip(index);

                break;
            case (EquipType.Gun):
                equippedGuns[index].equipping = false;
                equippedGuns[index] = null;
                _inventoryUI.gunPanel.Unequip(index);

                break;
        }
    }
    
    public bool HasEmptySlot(EquipItem[] items)
    {
        foreach (var slot in items)
        {
            if (slot == null)
            {
                return true;
            }
        }
        return false;
    }

    public int GetEmptySlot(EquipItem[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if ( items[i] == null)
            {
                return i;
            }
        }
        return 0;
    }
    

    #endregion
}