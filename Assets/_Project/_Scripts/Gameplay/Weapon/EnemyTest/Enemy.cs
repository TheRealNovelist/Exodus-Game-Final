using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
   [SerializeField] private float health = 50f;

   public void Damage(float amount)
   {
      health -= amount;
      if (health <= 0f)
      {
         Die();
      }
   }

   void Die()
   {
      Destroy(gameObject);
   }
}
