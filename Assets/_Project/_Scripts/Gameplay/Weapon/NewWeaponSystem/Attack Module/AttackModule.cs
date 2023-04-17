using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public abstract class AttackModule : MonoBehaviour
    {
        public virtual void StartAttack(Weapon weapon, bool consumeAmmo = true)
        {

        }

        public virtual void HoldAttack(Weapon weapon, bool consumeAmmo = true)
        {
            
        }
        
        protected static void ConsumeAmmo(Weapon weapon, bool consumeAmmo)
        {
            if (consumeAmmo)
                weapon.ConsumeAmmo();
        }
    }
}
