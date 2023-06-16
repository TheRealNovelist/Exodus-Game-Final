using System;
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

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (itemList != null && itemList.Count > 0)
        {
            gameObject.name = $"Chest {itemList.GetRandom().itemName}";
        }
    }
#endif


    private void OnTriggerEnter(Collider other)
    {
        if (isOpen)
        {
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(WaitToOpenChest());
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

    private IEnumerator WaitToOpenChest()
    {
        OpenAnimation();
        yield return new WaitForSeconds(1.5f);

        OpenChest();
    }

    private void OpenChest()
    {
        isOpen = true;

        //Play sound
        audioManager?.PlayOneShot("ChestOpen");

        lootedItem = GetRandomItem();

        if (lootedItem.model)
        {
            GameObject go = Instantiate(lootedItem.model, itemHolder.transform);
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
                
                Inventory.Instance._inventoryUI.PopUpNoti(consumeItem,false);

                StartCoroutine(WaitToHideItem());
                harvested = true;
            }
        }
    }

    private void Harvest(Item item)
    {
        //Play sound
        audioManager?.PlayOneShot("ChestHarvest");
        Inventory.Instance.AddItem(item);
        harvested = true;
        StartCoroutine(WaitToHideItem());
    }


    IEnumerator WaitToHideItem()
    {
        yield return new WaitForSeconds(0.5f);
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