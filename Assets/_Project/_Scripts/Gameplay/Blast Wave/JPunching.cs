using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JPunching : IState
{

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    void IState.Update()
    {
        Update();
    }
}
