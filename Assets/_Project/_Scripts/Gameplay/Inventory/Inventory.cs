using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEvent;
    
    [Header("References")]
    [SerializeField] private EquipmentPanel gunPanel, abilityPanel;
    [SerializeField] private TextMeshProUGUI notiTMP;
    [SerializeField] private List<Item> allItemInInventory;
    [SerializeField] private ItemSlot[] itemSlots;
    [SerializeField] private GameObject equipOptions;

    [SerializeField] private float notiDuration = 2;
    
    public Dictionary<Item, ItemSlot> slotsByItems = new Dictionary<Item, ItemSlot>();

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

    private void RefreshUI()
    {
        for (int j = 0;j < itemSlots.Length; j++)
        {
            if(itemSlots[j]._item is not EquipItem) itemSlots[j].UpdateSlotUI();
            else itemSlots[j].UpdateSlotUI();
        }
    }

    private void RefreshUI(ItemSlot slot)
    {
        slot.UpdateSlotUI();
    }

    private void Start()
    {
        SetSlotItem();
        RefreshUI();
        equipOptions.SetActive(false);
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
        EquipmentPanel panel =null;
        if (equipItem.type == EquipType.Gun) panel = gunPanel;
        else if (equipItem.type == EquipType.Ability) panel = abilityPanel;
        
        if (panel.HasEmptySlot()!=null)
        {
            for (int i = 0; i < panel.equipSlots.Length; i++)
            {
                if ( panel.equipSlots[i]._item != null) continue;
                panel.Equip(panel.equipSlots[i],equipItem);
                break;
            }

            RefreshUI();
            return true;
        }

        return false;
    }

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
    

    

}
