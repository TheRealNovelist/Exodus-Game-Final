using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BlastWave : MonoBehaviour
{
    int pointsCount;
    int damage;
    float maxRadius;
    float speed;
    float startWidth;
    float force;

    private LineRenderer lineRenderer;
    private EdgeCollider2D _edgeCollider2D;
    private bool basting = false;

    public void Init(int pointsCount, float max, float speed, float start, float force, int damage)
    {
        this.pointsCount = pointsCount;
        this.maxRadius = max;
        this.speed = speed;
        this.startWidth = start;
        this.force = force;
        this.damage = damage;
    }

    private IEnumerator Blast()
    {
        float currentRadius = 0f;

        while (currentRadius < maxRadius)
        {
            currentRadius += Time.deltaTime * speed;
            Draw(currentRadius);
            yield return null;
        }
    }
    
    private List<CapsuleCollider> colliders = new List<CapsuleCollider>();

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = pointsCount + 1;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            colliders.Add(gameObject.AddComponent<CapsuleCollider>());
            colliders[i].isTrigger = true;
        }
        blasted = false;
        StartCoroutine(Blast());
    }

    public LayerMask LayerMask;

    /*private void Damage(float currentRadius)
    {
        if (blasted || damaged)
        {
            return;
        }

        Collider[] hittingObjects = Physics.OverlapSphere(transform.position, currentRadius, LayerMask);

        if (hittingObjects.Length == 0)
        {
            return;
        }

        for (int i = 0; i < hittingObjects.Length; i++)
        {
            if (Vector3.Distance(hittingObjects[i].transform.position, transform.position) < Vector3.Distance(transform.position, lineRenderer.GetPosition(1)))
            {
                continue;
            }

            if (hittingObjects[i].TryGetComponent(out Rigidbody rb))
            {
                Vector3 direction = (hittingObjects[i].transform.position - transform.position).normalized;

                rb.AddForce(direction * force, ForceMode.Impulse);
            }

            if (hittingObjects[i].TryGetComponent(out IDamageable damageable))
            {
                Debug.Log($"{Vector3.Distance(hittingObjects[i].transform.position, transform.position)}/ {Vector3.Distance(transform.position, lineRenderer.GetPosition(1))}");
                damageable.Damage(damage);
                damaged = true;
            }
        }
    }*/

    private bool blasted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DisableAllColliders();
            
            if (other.gameObject.TryGetComponent(out Rigidbody rb))
            {
                Vector3 direction = (other.gameObject.transform.position - transform.position).normalized;

                rb.AddForce(direction * force, ForceMode.Impulse);
            }

            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(damage);
            }
        }
    }

    private void DisableAllColliders()
    {
        foreach (CapsuleCollider c in colliders)
        {
            c.enabled = false;
        }
    }

    private void Draw(float currentRadius)
    {
        float angleBetweenPoints = 360f / pointsCount;
        for (int i = 0; i <= pointsCount; i++)
        {
            float angle = i * angleBetweenPoints * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f);
            Vector3 position = direction * currentRadius;
            lineRenderer.SetPosition(i, position);
            colliders[i].center = position;
            colliders[i].height =  lineRenderer.widthMultiplier ;
            colliders[i].direction =  0;
            colliders[i].radius =  0.05f ;
        }


        lineRenderer.widthMultiplier = Mathf.Lerp(0f, startWidth, 1f - currentRadius / maxRadius);

        if (lineRenderer.widthMultiplier == 0)
        {
            blasted = true;
            Destroy(gameObject);
        }
    }

}