using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EquipType
{
    Gun,
    Ability
}

[CreateAssetMenu(menuName = "Item/Equip Item/")]
public class EquipItem : Item
{
    [HideInInspector] public EquipType Type;
    public bool unlocked = false;
    public bool equipping = false;

   
}
