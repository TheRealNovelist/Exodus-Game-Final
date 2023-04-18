using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class SOInjector : WeaponDataInjector
    {
        [SerializeField] private WeaponDataSO dataSO;

        public override WeaponData TryGetData()
        {
            return dataSO != null ? dataSO.GetData() : new WeaponData();
        }
    }
}
