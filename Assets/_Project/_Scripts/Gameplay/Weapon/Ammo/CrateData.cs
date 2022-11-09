using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New crate", menuName = "Crate")]
public class CrateData : ScriptableObject
{
    public string crateAmmoType; //type of ammo that will be store in side crate
    public int ammoAmount;
    public int ammoType; //store ammo type by int
}
