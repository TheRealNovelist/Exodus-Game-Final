using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using WeaponSystem;


[CreateAssetMenu(menuName = "Item/Equip Item/Gun Item")]
public class GunItem : EquipItem
{
    [FormerlySerializedAs("Injector")] public WeaponDataSO gunData;
    
    private void Awake()
    {
        Type = EquipType.Gun;
    }
}
