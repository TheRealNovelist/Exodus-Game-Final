using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _damage = 0;
    [SerializeField] private AudioManager audiomanager;
    public void Init(float damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter(Collider collision)
    {
      
        if (collision.gameObject.TryGetComponent(out IDamageable hitObject))
        {
            audiomanager.PlayOneShot("DamagerPlayerSound");
            hitObject.Damage(_damage, transform);
        }
        else
        {
            audiomanager.PlayOneShot("BulletHit");
        }
        
        Destroy(gameObject);
    }
}
