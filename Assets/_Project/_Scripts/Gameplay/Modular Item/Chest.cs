using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private SpriteRenderer itemHolder;
    [SerializeField] private WeightedRandomList<Item> itemList;
    [SerializeField] private KeyCode openKey;

    private bool isOpen = false;
    private bool harvested = false;
    
    private Item lootedItem;
    

    private void OnTriggerEnter(Collider other)
    {
        if(isOpen){return;}
        if (other.gameObject.CompareTag("Player"))
        {
            OpenChest();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!harvested && isOpen)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (Input.GetKeyDown(openKey))
                {
                    Harvest(lootedItem);
                }
            }
        }
    }

    private void OpenChest()
    {
        isOpen = true;

        lootedItem = GetRandomItem();

        itemHolder.sprite = lootedItem.icon;

        if (lootedItem is not EquipItem)
        {
            ConsumableItem consumeItem = lootedItem as ConsumableItem;
            if (consumeItem.storeInInventory)
            {
                Harvest(consumeItem);
            }
            else
            {
                if (consumeItem.effect)
                {
                    Debug.Log("hhhh");
                    consumeItem.effect.Invoke();
                }
                else
                {
                    Debug.LogWarning($"Item {consumeItem.name} has no effect event");
                }
            }
        }
    }

    private void Harvest(Item item)
    {
        harvested = true;
        Inventory.Instance.AddItem(item);
    }

    private Item GetRandomItem()
    {
       return itemList.GetRandom();
    }
    

}
