using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    [DisallowMultipleComponent]
    public abstract class AttackModule : MonoBehaviour
    {
        public abstract void Attack(float damage);
    }
}
