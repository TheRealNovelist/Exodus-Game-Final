using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Null State", menuName = "Enemy AI/State/Null State", order = 0)]
public class NullState : State
{
    // State have no callback to facilitate a null check
    public override void UpdateState(EnemyBrain brain)
    {
        
    }
}
