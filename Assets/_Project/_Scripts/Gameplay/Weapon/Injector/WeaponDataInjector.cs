using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public abstract class WeaponDataInjector : MonoBehaviour
    {
        public abstract WeaponData TryGetData();
    }
}
