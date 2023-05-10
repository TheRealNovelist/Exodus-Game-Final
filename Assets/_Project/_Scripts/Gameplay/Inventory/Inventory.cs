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
    [Header("Equip Panels")]
    [SerializeField] private GameEvent equipAbilityEvent ;
    [SerializeField] private GameEvent equipGunEvent ;
    public EquipmentPanel gunPanel, abilityPanel;
    
    [Header("Items and Slots")]
    [SerializeField] private List<Item> allItemInInventory;
    [SerializeField] private List<ItemSlot> itemSlots;
    


    [Header("Notification")]
    [SerializeField] private TextMeshProUGUI notiTMP;
    [SerializeField] private float notiDuration = 2;
    
    [Header("Equip Options")]
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private Vector3 offset;

    [SerializeField] private GameObject inventoryPanel;
    
    [HideInInspector] public EquipItem currentSelecting;

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
        if (Input.GetKey(KeyCode.Tab))
        {
            inventoryPanel.SetActive(true);
        } 
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            inventoryPanel.SetActive(false);
            optionPanel.SetActive(false);

        } 
    }

    #region Inventory
    public void SetSlotItem()
    {
        int j = 0;
        for (; j < allItemInInventory.Count && j < itemSlots.Count; j++)
        {
            itemSlots[j]._item = allItemInInventory[j];
            slotsByItems.Add(allItemInInventory[j], itemSlots[j]);
        }
        
        for (; j < itemSlots.Count; j++)
        {
            itemSlots[j]._item =null;
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
        PopUpNoti(item);
    }

    private void AddToInventory(Item item)
    {
        slotsByItems[item].amount++;
        RefreshUI(slotsByItems[item]);
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
    #endregion

    #region Slot UI
    private void RefreshUI(ItemSlot slot)
    {
        item.equipping = true;

        switch (item.Type)
        {
            if(itemSlots[j]._item is not EquipItem) itemSlots[j].UpdateSlotUI();
            else itemSlots[j].UpdateSlotUI();
        }
    }

            case (EquipType.Gun):
                equippedGuns[index] = item;
                _inventoryUI.gunPanel.Equip(index, item);
                GunItem gun = item as GunItem;
                OnGunEquiped?.Invoke(gun.gunData,index);
                break;
        }
    }

    IEnumerator WaitToHideNoti()
    {
        yield return new WaitForSeconds(notiDuration);
        notiTMP.text = "";
    }
    #endregion   

    #region Equip Option Panel

    public void EquipItemSlotSelected(bool show = true)
    { 
        var selectedButton = EventSystem.current.currentSelectedGameObject;

        var selectedSlot = selectedButton.GetComponent<ItemSlot>();
        var itemOfSelectedSlot = slotsByItems.FirstOrDefault(item => item.Value == selectedSlot).Key;
        EquipItem selectedEquipment = itemOfSelectedSlot as EquipItem;

        if (selectedEquipment != null && selectedEquipment.unlocked && selectedEquipment.equipping == false)
        {
            optionPanel.gameObject.SetActive(show);
            optionPanel.GetComponent<RectTransform>().position = selectedButton.GetComponent<RectTransform>().position + offset;
            currentSelecting = itemOfSelectedSlot as EquipItem;
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
