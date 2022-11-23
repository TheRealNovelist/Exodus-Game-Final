using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Ability/Medkit")]
public class RestoreHealth : SkillSystem
{
    public override void Activate(GameObject parent)
    {
        PlayerHealth playerHealth = parent.GetComponent<PlayerHealth>();
        playerHealth.AddHealth(4);
        //shieldHolder.SetActive(true);
    }
   
}
