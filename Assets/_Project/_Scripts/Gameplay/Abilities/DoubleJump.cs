using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoubleJump", menuName = "Ability/DoubleJump")]
public class DoubleJump : SkillSystem
{
    public float jumpForce=100f;
    public override void Activate(GameObject parent)
    {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        Rigidbody rb = parent.GetComponent<Rigidbody>();
        
        if (!movement.isGrounded)
        {
            rb.AddForce(movement.transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    public override void BeginCooldown(GameObject parent)
    {
        base.BeginCooldown(parent);
    }
}
