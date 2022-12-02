using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Consumable Item")]
public class ConsumableItem : Item
{
    public bool storeInInventory = true;
    public GameEvent effect;
}
