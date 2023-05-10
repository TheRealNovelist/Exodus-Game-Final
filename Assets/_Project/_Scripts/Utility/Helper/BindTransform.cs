using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BindTransform : MonoBehaviour
{
    [SerializeField] private Transform followTransform;
    
    void Update()
    {
        if (Application.IsPlaying(gameObject))
        {
            enabled = false;
            return;
        }
        
        if (transform != null)
            transform.SetPositionAndRotation(followTransform.position, followTransform.rotation);
    }
}
