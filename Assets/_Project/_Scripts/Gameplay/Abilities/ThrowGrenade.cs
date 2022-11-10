using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Grenade", menuName = "Ability/Grenade")]
public class ThrowGrenade : SkillSystem
{
    Grenade grenade;
    public GameObject grenadePrefab;

    public override void Activate(GameObject parent)
    {
        GameObject newGre = Instantiate(grenadePrefab);
        newGre.GetComponent<Grenade>().GrenadeThrow();
    }

    public override void BeginCooldown(GameObject parent)
    {
        base.BeginCooldown(parent);
    }
}
