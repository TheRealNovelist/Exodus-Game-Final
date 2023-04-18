using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public abstract class Modifier : ScriptableObject
    {
        public abstract WeaponData Modify(WeaponData data);
    }
}
