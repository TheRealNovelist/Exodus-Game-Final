using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    private Vector3 shootDirect;
    private int _damage = 10;
    
    
    // Update is called once per frame
    void Update()
    {
       // ShootDirect(shootDirect);
    }

    public void ShootDirect(Vector3 direct)
    {
        transform.position += direct * 100 * Time.deltaTime;
    }

    public void Init(Vector3 direct,int damage)
    {
        _damage = damage;
        this.shootDirect = direct;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("in");

            if (collision.gameObject.TryGetComponent(out IDamageable idamageable))
            {
                Debug.Log("damage");

                idamageable.Damage(_damage);
            }
        }
        
        
        Destroy(gameObject);
    }
}
