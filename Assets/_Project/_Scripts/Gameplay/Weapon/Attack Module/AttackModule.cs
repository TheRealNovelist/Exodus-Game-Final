using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public abstract class AttackModule : MonoBehaviour
    {
        public LayerMask AttackMask;

        public virtual void StartAttack(Weapon weapon, bool consumeAmmo = true)
        {

        }

        public virtual void StopAttack()
        {
            
        }
        
        protected void ConsumeAmmo(Weapon weapon, bool consumeAmmo)
        {
            if (consumeAmmo)
                if (!weapon.TryConsumeAmmo())
                {
                    Debug.Log("tried");
                    StopAttack();
                }
                
        }
    }
}
