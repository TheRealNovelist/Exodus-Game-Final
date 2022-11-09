using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : ScriptableObject
{
    [SerializeField] private Transition[] transitions;

    public virtual void EnterState(EnemyBrain brain)
    {
        
    }
    
    public virtual void UpdateState(EnemyBrain brain)
    {
        CheckTransition(brain);
    }
    
    public virtual void ExitState(EnemyBrain brain)
    {
        
    }

    private void CheckTransition(EnemyBrain brain)
    {
        foreach (Transition transition in transitions)
        {
            bool decisionSucceeded = transition.decision.Decide(brain);
            
            brain.TransitionToState(decisionSucceeded ? transition.trueState : transition.falseState);
        }
    }
}
