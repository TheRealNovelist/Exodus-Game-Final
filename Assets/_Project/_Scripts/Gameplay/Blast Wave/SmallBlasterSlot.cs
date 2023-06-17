using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBlasterSlot : MonoBehaviour
{
    private SmallBlaster _blaster;

    public void Init(SmallBlaster blaster)
    {
        _blaster = blaster;
    }

    public void ClearSlot()
    {
        if (_blaster)
        {
            Destroy(_blaster.gameObject);
        }
    }

    public bool SlotAvailable => _blaster == null;
}