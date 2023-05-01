using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.Serialization;
using WeaponSystem;

public class Inventory : Singleton<Inventory>
{
    [Header("Items and Slots")] [SerializeField]
    private List<Item> allItemInInventory;

    public InventoryUI _inventoryUI;

    public Dictionary<Item, ItemSlot> slotsByItems = new Dictionary<Item, ItemSlot>();

    [Header("Equip Panels")] public GameEvent equipAbilityEvent;

    public Action<WeaponDataSO, int> OnGunEquiped;
    //public GameEvent equipGunEvent;

    [FormerlySerializedAs("equippedItems")] public EquipItem[] equippedGuns = new EquipItem[2];
    [FormerlySerializedAs("equippedItems")] public EquipItem[] equippedAbilities = new EquipItem[2];


    private void Start()
    {
        SetSlotItem();
        CheckEquipmentOnStart(equippedGuns);
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

        switch (equipItem.Type)
        {
            case (EquipType.Ability):
                if (TryGetEmptySlot(equippedAbilities, out int index))
                {
                    Equip(index, equipItem);
                    return true;
                }

                break;
            case (EquipType.Gun):
                if (TryGetEmptySlot(equippedGuns, out index))
                {
                    Equip(index, equipItem);
                    return true;
                }
                break;
        }

        return false;
    }

    public void Equip(int index, EquipItem item)
    {
        item.equipping = true;

        switch (item.Type)
        {
            case (EquipType.Ability):
                equippedAbilities[index] = item;
                _inventoryUI.abilityPanel.Equip(index, item);
                equipAbilityEvent.Invoke();
                break;

            case (EquipType.Gun):
                equippedGuns[index] = item;
                _inventoryUI.gunPanel.Equip(index, item);
                GunItem gun = item as GunItem;
                OnGunEquiped?.Invoke(gun.gunData,index);
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
    
    public bool TryGetEmptySlot(EquipItem[] items, out int index)
    {
        index = -1;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null) {
                index = i;
                return true;
            }
        }
        return false;
    }

    private void CheckEquipmentOnStart(EquipItem[] items)
    {
        for (var index = 0; index < items.Length; index++)
        {
            var i = items[index];
            if (i != null)
            {
                var item = i;
                items[index]= null;

                AddEquipItem(item);
            }
        }
    }

    public int EquipedGunsQuantity()
    {
        int i = 0;

        foreach (EquipItem gun in equippedGuns)
        {
            if (gun != null)
            {
                i++;
            }
            else
            {
                return i;
            }
        }

        return i;
    }
    

    #endregion
}