using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    private Vector3 shootDirect;
    private int _damage = 10;
    public GameObject particleSystemPrefab;
    
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
        Vector3 contactPoint = collision.contacts[0].point;
        Quaternion rotation = Quaternion.LookRotation(collision.contacts[0].normal);

        GameObject particleSystem = Instantiate(particleSystemPrefab, contactPoint, rotation);
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
