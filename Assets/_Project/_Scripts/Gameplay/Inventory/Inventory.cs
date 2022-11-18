using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;


public class Inventory : MonoBehaviour
{
    [Header("Equip Panels")]
    [SerializeField] private GameEvent equipAbilityEvent ;
    [SerializeField] private GameEvent equipGunEvent ;
    public EquipmentPanel gunPanel, abilityPanel;
    
    [Header("Items and Slots")]
    [SerializeField] private List<Item> allItemInInventory;
    [SerializeField] private ItemSlot[] itemSlots;

    [Header("Notification")]
    [SerializeField] private TextMeshProUGUI notiTMP;
    [SerializeField] private float notiDuration = 2;
    
    [Header("Equip Options")]
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private Vector3 offset;

    [SerializeField] private GameObject inventoryPanel;
    
    [HideInInspector] public EquipItem currentSelecting;

    public Dictionary<Item, ItemSlot> slotsByItems = new Dictionary<Item, ItemSlot>();

    private void Start()
    {
        SetSlotItem();
        RefreshUI();
        optionPanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            inventoryPanel.SetActive(true);
            CursorAndCam.Instance.InventoryPanelOn = true;
            CursorAndCam.Instance.UnlockCursor();
        } 
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            inventoryPanel.SetActive(false);
            optionPanel.SetActive(false);
            CursorAndCam.Instance.InventoryPanelOn = false;
            CursorAndCam.Instance.LockCursor();
            
        } 
    }

    #region Inventory
    public void SetSlotItem()
    {
        int j = 0;
        for (; j < allItemInInventory.Count && j < itemSlots.Length; j++)
        {
            itemSlots[j]._item = allItemInInventory[j];
            slotsByItems.Add(allItemInInventory[j], itemSlots[j]);
        }
        
        for (; j < itemSlots.Length; j++)
        {
            itemSlots[j]._item =null;
        }
    }

    public void AddItem(Item item)
    {
        var equipItem = item as EquipItem;
        if (equipItem is not null && AddEquipItem(equipItem))
        {
            //Highlight as using item
        }

        AddToInventory(item);
        PopUpNoti(item);
    }

    private void AddToInventory(Item item)
    {
        slotsByItems[item].amount++;
        RefreshUI(slotsByItems[item]);
    }

    private bool AddEquipItem(EquipItem equipItem)
    {
        equipItem.unlocked = true;
        
        switch (equipItem.type)
        {
            case(EquipType.Ability):
                if (abilityPanel.HasEmptySlot())
                {
                    abilityPanel.Equip(abilityPanel.GetEmptySlot(), equipItem);
                    equipAbilityEvent.Invoke();
                    RefreshUI();
                    return true;
                }
                break;
            case(EquipType.Gun):
                if (gunPanel.HasEmptySlot())
                {
                    gunPanel.Equip(gunPanel.GetEmptySlot(), equipItem);
                    equipGunEvent.Invoke();
                    RefreshUI();
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
        slot.UpdateSlotUI();
    }
    
    private void RefreshUI()
    {
        for (int j = 0;j < itemSlots.Length; j++)
        {
            if(itemSlots[j]._item is not EquipItem) itemSlots[j].UpdateSlotUI();
            else itemSlots[j].UpdateSlotUI();
        }
    }
    #endregion

    #region Inventory Notification
    private void PopUpNoti(Item itemAdded)
    {
        notiTMP.text = itemAdded.name + " added to inventory";
        StartCoroutine(WaitToHideNoti());
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
        else
        {
            optionPanel.gameObject.SetActive(!show);
        }
    }

    public void EquipOption(int index)
    {
        switch (currentSelecting.type)
        {
            case (EquipType.Ability):
                abilityPanel.Unequip(index);
                abilityPanel.Equip(index,currentSelecting);
                equipAbilityEvent.Invoke();
                break;
            case (EquipType.Gun):
                gunPanel.Unequip(index);
                gunPanel.Equip(index,currentSelecting);
                equipGunEvent.Invoke();
                break;
        }
        
        optionPanel.SetActive(false);
    }

    #endregion

}
