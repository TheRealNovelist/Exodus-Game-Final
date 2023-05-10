using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Equip Item/Ability Item")]
public class AbilityItem : EquipItem
{
    public SkillSystem ability;
    
    private void Awake()
    {
        Type = EquipType.Ability;
    }
    
}
