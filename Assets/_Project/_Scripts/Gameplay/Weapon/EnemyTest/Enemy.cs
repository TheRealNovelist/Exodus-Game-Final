using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
   [SerializeField] private float health = 50f;

   public void TakeDamage(float gunDamage)
   {
      health -= gunDamage;
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
