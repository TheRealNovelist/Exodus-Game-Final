using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EnemySystem;
using UnityEngine;

public class JBlasting : IState
{
    private LineRenderer lineRenderer;

    private Juggernaut _enemy;
    private LineRenderer _lineRenderer;

    private float _chargeTime;

    public bool Blasted = false;
    private bool hitTargetMask = false;

    public JBlasting(Juggernaut enemy)
    {
        _enemy = enemy;
    }

    float currentRadius = 0f;
    private bool countingDown = false;
    private bool waved = false;

    // Update is called once per frame
    void Update()
    {
        if (countingDown)
        {
            if (_chargeTime <= 0f)
            {
                countingDown = false;
                float y = _enemy.transform.position.y;
                _enemy.transform.DOMoveY(y + 6, 1f).OnComplete(() =>
                {
                    _enemy.transform.DOMoveY(y, 0.5f).OnComplete(() =>
                    {
                        waved = true;
                    });
                });
            }

            _chargeTime -= Time.deltaTime;
        }

        if (waved)
        {
            if (currentRadius < _enemy.MaxRadius)
            {
                currentRadius += Time.deltaTime * _enemy.BlastSpeed;
                Draw(currentRadius);
                Damage(currentRadius);
            }
        }
    }

    public void OnEnter()
    {
        Debug.Log("blast eneter");
        lineRenderer = _enemy.BlastPos.GetComponent<LineRenderer>();

        lineRenderer.positionCount = _enemy.pointsCount + 1;

        Blasted = false;
        hitTargetMask = false;
        waved = false;
        countingDown = true;
        _chargeTime = _enemy.ChargeTime;
        currentRadius = 0f;
    }

    public void OnExit()
    {
    }

    void IState.Update()
    {
        Update();
    }

    private void Damage(float currentRadius)
    {
        if (hitTargetMask)
        {
            return;
        }

        Collider[] hittingObjects = Physics.OverlapSphere(_enemy.BlastPos.position, currentRadius, _enemy.blastMask);

        if (hittingObjects.Length > 0)
        {
            Debug.Log("hit target");
            hitTargetMask = true;
            for (int i = 0; i < hittingObjects.Length; i++)
            {
                if (hittingObjects[i].TryGetComponent(out Rigidbody rb))
                {
                    Vector3 direction = (hittingObjects[i].transform.position - _enemy.BlastPos.position).normalized;

                    rb.AddForce(direction * _enemy.force, ForceMode.Impulse);
                }

                if (hittingObjects[i].TryGetComponent(out IDamageable damageable))
                {
                    damageable.Damage(_enemy.DamageDealth);
                }
            }
        }
    }

    private void Draw(float currentRadius)
    {
        float angleBetweenPoints = 360f / _enemy.pointsCount;

        for (int i = 0; i <= _enemy.pointsCount; i++)
        {
            float angle = i * angleBetweenPoints * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f);
            Vector3 position = direction * currentRadius;

            lineRenderer.SetPosition(i, position);
        }

        lineRenderer.widthMultiplier = Mathf.Lerp(0f, _enemy.startWidth, 1f - currentRadius / _enemy.MaxRadius);
        if (lineRenderer.widthMultiplier == 0)
        {
            Blasted = true;
        }
    }
}