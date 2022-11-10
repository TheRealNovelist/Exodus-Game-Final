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
        GameObject newGre = Instantiate(grenadePrefab,parent.transform.position,Quaternion.identity);
        newGre.GetComponent<Rigidbody>().AddForce(newGre.transform.forward * 10);
    }

    public override void BeginCooldown(GameObject parent)
    {
        base.BeginCooldown(parent);
    }
}
