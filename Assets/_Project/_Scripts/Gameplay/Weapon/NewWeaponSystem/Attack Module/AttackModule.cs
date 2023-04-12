using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public abstract class AttackModule : MonoBehaviour
    {
        public abstract void Attack(WeaponData data);

        public virtual void EndAttack() {}
    }
}
