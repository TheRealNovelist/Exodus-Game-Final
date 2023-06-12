using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastWave : MonoBehaviour
{
    [SerializeField] int pointsCount;
    [SerializeField] float maxRadius;
    [SerializeField] float speed;
    [SerializeField] float startWidth;
    [SerializeField] float force;
    [SerializeField] private LayerMask _mask;

    private LineRenderer lineRenderer;

    private bool basting = false;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = pointsCount + 1;
    }

    private IEnumerator Blast()
    {
        float currentRadius = 0f;

        while (currentRadius < maxRadius)
        {
            currentRadius += Time.deltaTime * speed;
            Draw(currentRadius);
            Damage(currentRadius);
            yield return null;
        }
    }

    private void Start()
    {
        basting = false;
    }

    private void Damage(float currentRadius)
    {
        Collider[] hittingObjects = Physics.OverlapSphere(transform.position, currentRadius, _mask);

        if (hittingObjects.Length == 0)
        {
            return;
        }

        Debug.Log($"total {hittingObjects.Length}");

        for (int i = 0; i < hittingObjects.Length ; i++)
        {
            Debug.Log(i);

            if (hittingObjects[i].TryGetComponent(out Rigidbody rb))
            {
                Vector3 direction = (hittingObjects[i].transform.position - transform.position).normalized;

                rb.AddForce(direction * force, ForceMode.Impulse);
            }
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
        }

        lineRenderer.widthMultiplier = Mathf.Lerp(0f, startWidth, 1f - currentRadius / maxRadius);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(Blast());
        }
    }
}