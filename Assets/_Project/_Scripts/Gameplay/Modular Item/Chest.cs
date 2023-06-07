using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private SpriteRenderer itemHolder;
    [SerializeField] private WeightedRandomList<Item> itemList;
    [SerializeField] private KeyCode openKey;
    [SerializeField] private Animator _chestAnimator;

    [SerializeField] private AudioManager audioManager;

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

        OpenAnimation();
        
        //Play sound
        audioManager?.PlayOneShot("ChestOpen");
        StartCoroutine(WaitToGenerateIte());
    }

    IEnumerator WaitToGenerateIte()
    {
        lootedItem = GetRandomItem();

        yield return new WaitForSeconds(0.5f);

        if (lootedItem.model)
        {
          GameObject go =  Instantiate(lootedItem.model, itemHolder.transform);
          go.transform.parent = itemHolder.transform;
          go.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
        else
        {
            itemHolder.sprite = lootedItem.icon;
        }

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
        //Play sound
        audioManager?.PlayOneShot("ChestHarvest");
        harvested = true;
        Inventory.Instance.AddItem(item);
        itemHolder.gameObject.SetActive(false);
    }

    private Item GetRandomItem()
    {
       return itemList.GetRandom();
    }
    
    public void OpenAnimation()
    {
        _chestAnimator.SetTrigger("openTrigger");
        itemHolder.gameObject.SetActive(true);
        _chestAnimator.SetBool("isOpen", true);
    }

    public void CloseAnimation()
    {
        _chestAnimator.SetTrigger("closeTrigger");
        _chestAnimator.SetBool("isOpen", false);
    }
    

}
