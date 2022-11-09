using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Ability/Dash")]
public class Dash : SkillSystem
{
    public float dashVelocity;

    public override void Activate(GameObject parent)
    {
        PlayerMove1 movement = parent.GetComponent<PlayerMove1>();
        Rigidbody rb = parent.GetComponent<Rigidbody>();

       // rb.velocity = movement.transform.position.normalized * dashVelocity;
       rb.velocity = movement.lastMoveDirection.normalized * dashVelocity; 
      
      
    }

    public override void BeginCooldown(GameObject parent)
    {
        PlayerMove1 movement = parent.GetComponent<PlayerMove1>();
        base.BeginCooldown(parent);
    }
}
