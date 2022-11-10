using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Patrol State", menuName = "Enemy AI/State/Patrol State")]
public class PatrolState : State
{
    public override void UpdateState(EnemyBrain brain)
    {
        base.UpdateState(brain);
        
        
    }
}
