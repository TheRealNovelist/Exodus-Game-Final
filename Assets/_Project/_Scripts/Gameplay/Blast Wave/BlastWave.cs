using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class BlastWave : MonoBehaviour
{
    int pointsCount;
    int damage;
    float maxRadius;
    float speed;
    float startWidth;
    float force;

    private LineRenderer lineRenderer;
    private MeshCollider _meshCollider;
    

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
          //  Damage(currentRadius);
            yield return null;
        }
    }


    private void Start()
    {
        Init(25, 25, 10, 1, 20,10);
        lineRenderer = GetComponent<LineRenderer>();
        _meshCollider = GetComponent<MeshCollider>();
        mesh = new Mesh();
        lineRenderer.positionCount = pointsCount + 1;
        basting = false;
        StartCoroutine(Blast());
    }

    private void Damage(float currentRadius)
    {
        Collider[] hittingObjects = Physics.OverlapSphere(transform.position, currentRadius);

        if (hittingObjects.Length == 0)
        {
            return;
        }

        for (int i = 0; i < hittingObjects.Length; i++)
        {
            if (hittingObjects[i].TryGetComponent(out Rigidbody rb))
            {
                Vector3 direction = (hittingObjects[i].transform.position - transform.position).normalized;

                rb.AddForce(direction * force, ForceMode.Impulse);
            }
        }
    }

    Mesh mesh ;

    private void Draw(float currentRadius)
    {
        List<Vector2> edges = new List<Vector2>();
        float angleBetweenPoints = 360f / pointsCount;

        for (int i = 0; i <= pointsCount; i++)
        {
            float angle = i * angleBetweenPoints * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f);
            Vector3 position = direction * currentRadius;
            lineRenderer.SetPosition(i, position);

        }

        lineRenderer.widthMultiplier = Mathf.Lerp(0f, startWidth, 1f - currentRadius / maxRadius);
        
        
        lineRenderer.BakeMesh(mesh);
        _meshCollider.sharedMesh = mesh;
      //  Debug.Log($"{_meshCollider.bounds.extents} / {lineRenderer.widthMultiplier}");

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(damage);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(damage);
            }
        }
    }
}