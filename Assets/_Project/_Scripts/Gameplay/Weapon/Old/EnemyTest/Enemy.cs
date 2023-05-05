using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Old
{
   public class Enemy : MonoBehaviour, IDamageable
   {
      [SerializeField] private float health = 50f;

      public void Damage(float amount, Transform source = null)
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

}