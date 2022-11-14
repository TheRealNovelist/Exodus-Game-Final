using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoubleJump", menuName = "Ability/DoubleJump")]
public class DoubleJump : SkillSystem
{
    PlayerMove1 playerMovement;
    public float jumpForce;
    public override void Activate(GameObject parent)
    {
        PlayerMove1 movement = parent.GetComponent<PlayerMove1>();
        Rigidbody rb = parent.GetComponent<Rigidbody>();
        if (playerMovement.isInAir)
        {
            rb.AddForce(movement.transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    public override void BeginCooldown(GameObject parent)
    {
        base.BeginCooldown(parent);
    }
}
