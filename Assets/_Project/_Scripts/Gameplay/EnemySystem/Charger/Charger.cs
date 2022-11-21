using System.Collections;
using System.Collections.Generic;
using EnemySystem;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem.Charger
{
    public class Charger : BaseEnemy
    {
        [Header("Components")] 
        [SerializeField] private NavMeshAgent agent;
        
        [Header("Settings")]
        [SerializeField] private float attackRange = 2f;
        public float painMultiplier = 1f;
        public float attackCooldown = 5f;
        public float damageDealt = 10f;
        
        
        
    }
}
