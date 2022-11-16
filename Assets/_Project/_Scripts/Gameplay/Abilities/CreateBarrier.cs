using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Barrier", menuName = "Ability/Barrier")]
public class CreateBarrier : SkillSystem
{
    public GameObject shieldPrefab;
    public GameObject shieldHolderT;
    public string shieldName;
    public override void Activate(GameObject parent)
    {
        shieldHolderT = GameObject.Find(shieldName);
        GameObject newShield = Instantiate(shieldPrefab, parent.transform.position, Quaternion.identity);
        Debug.Log("Barrier");
        if (activeTime <= 0)
        {
            Debug.Log("Shield Destroyed");
            Destroy(newShield);
        }
    }

    public override void BeginCooldown(GameObject parent)
    {
        base.BeginCooldown(parent);
    }
}
