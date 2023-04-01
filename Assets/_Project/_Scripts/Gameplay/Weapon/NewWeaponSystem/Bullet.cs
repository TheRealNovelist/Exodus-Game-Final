using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _damage = 0;
    
    public void Init(float damage)
    {
        _damage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable hitObject))
        {
            hitObject.Damage(_damage);
        }
        
        Destroy(gameObject);
    }
}
