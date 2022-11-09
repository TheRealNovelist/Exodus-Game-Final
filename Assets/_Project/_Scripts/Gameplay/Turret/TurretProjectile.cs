using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    private Vector3 shootDirect;
    public float speed = 10f;

    public void SetDirect(Vector3 direct)
    {
        this.shootDirect = direct;
    }
    
    // Update is called once per frame
    void Update()
    {
        ShootDirect(shootDirect,speed);
    }

    public void ShootDirect(Vector3 direct, float speed = 10f)
    {
        transform.position += direct * speed * Time.deltaTime;
    }
}
