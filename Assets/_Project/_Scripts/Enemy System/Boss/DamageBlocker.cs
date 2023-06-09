using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBlocker : MonoBehaviour, IDamageable
{
    public void Damage(float amount, Transform source = null)
    {
        Debug.Log("Damage Blocked");
    }
}
