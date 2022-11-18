using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public GameObject shieldHolderT;
    public string shieldName;
    public float shieldHP = 100f;
    public float shieldActiveTime = 3f;
    float countDown;
    // Start is called before the first frame update
    void Start()
    {
        countDown = shieldActiveTime;
        shieldHolderT = GameObject.Find(shieldName);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = shieldHolderT.transform.position;
        countDown -= Time.deltaTime;

        if (countDown <= 0)
        {
            DestroyShield();
        }
    }

    void DestroyShield()
    {
        Debug.Log("Shield Destroyed");
        Destroy(this.gameObject);
    }
}
