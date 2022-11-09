using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private Transform itemHolder;
    public WeightedRandomList<Item> itemList;
    public KeyCode openKey;
    private bool isOpen = false;
    private bool harvested = false;
    private Item lootedItem;
    [SerializeField] private Inventory inventory;

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
        
        //PLAY ANIMATION

        lootedItem = GetRandomItem();

        //Instantiate(lootedItem.model, itemHolder);

        if (lootedItem is not EquipItem)
        {
            Harvest(lootedItem);
        }
    }

    private void Harvest(Item item)
    {
        harvested = true;
        //ADD ITEM TO INVENTORY
        inventory.AddItem(item);
    }

    private Item GetRandomItem()
    {
       return itemList.GetRandom();
    }

}
