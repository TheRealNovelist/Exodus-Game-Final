using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace EnemySystem.Brute.Charger
{
    public class Charger : Brute
    {
        [Header("Charger Settings")]
        [SerializeField] private float damageMultiplier = 0.2f;
        [SerializeField] private float chargedDamageDealt = 20f;
        
        public Transform chargeCastOrigin;
        public float chargeCastRadius;
        public LayerMask chargeMask;

        [Space]
        [SerializeField] private float checkInterval = 5f;
        [SerializeField] private float chargeAttackPercentage = 40f;

        [SerializeField] public bool initiatedChargeAttack;
        private float timer;

        private ChargeAttack ChargeAttack;

        protected override void Awake()
        {
            base.Awake();
            timer = checkInterval;
        }

        [Button]
        public override void StartStateMachine(float delay = 0f)
        {
            if (IsStateMachineStarted()) return;
            
            base.StartStateMachine(delay);
            
            var Charging = new Charging(this);
                ChargeAttack = new ChargeAttack(this, agent);
                
            AddTransition(MoveToTarget, Charging, () => initiatedChargeAttack);
        }

        protected override void OnStateMachineUpdate()
        {
            base.OnStateMachineUpdate();
            
            CheckForChargeAttack();
        }

        public void FinishCharge()
        {
            SetState(ChargeAttack);
        }

        private void CheckForChargeAttack()
        {
            if (initiatedChargeAttack) return;

            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }

            timer = checkInterval;
            initiatedChargeAttack = Random.Range(0, 100f) <= chargeAttackPercentage;
        }

        public void ChargedAttack()
        {
            if (target.TryGetComponent(out IDamageable targetDamage))
            {
                targetDamage.Damage(chargedDamageDealt);
            }
        }

        public void ReduceDamage(bool isReducing)
        {
            float multiplier = isReducing ? damageMultiplier : 1f;
            Health.SetDamageMultiplier(multiplier);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (initiatedChargeAttack) return;
            
            EnemyAnimator.SetTrigger("Cooldown");
            initiatedChargeAttack = false;
        }
    }
}
