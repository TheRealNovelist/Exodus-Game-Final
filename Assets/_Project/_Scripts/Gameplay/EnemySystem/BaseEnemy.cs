using System;
using UnityEngine;

namespace EnemySystem
{
    public abstract class BaseEnemy : BaseAI, IDamageable
    {
        [Header("Base Settings")] 
        public float maxHealth;
        
        public Transform target;

        private float health;

        public virtual void Damage(float amount)
        {
            health -= maxHealth;
        }
    }
}
