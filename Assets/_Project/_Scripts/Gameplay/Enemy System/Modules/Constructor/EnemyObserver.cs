using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyObserver : MonoBehaviour
{
    public Transform target;
    public LayerMask detectionMask;
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public bool IsCurrentlyTargeting()
    {
        return target;
    }
}
