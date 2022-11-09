using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Look Decision", menuName = "Enemy AI/Decision/Look")]
public class LookDecision : Decision
{
    public override bool Decide(EnemyBrain brain)
    {
        return brain.Observer.IsCurrentlyTargeting();
    }
}
