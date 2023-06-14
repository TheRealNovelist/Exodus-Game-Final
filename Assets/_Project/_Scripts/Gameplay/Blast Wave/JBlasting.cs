using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EnemySystem;
using Unity.VisualScripting;
using UnityEngine;

public class JBlasting : IState
{
    private LineRenderer lineRenderer;

    private Juggernaut _enemy;
    private LineRenderer _lineRenderer;

    private float _chargeTime;

    public bool Blasted = false;
    private bool _damage = false;
    private bool _charging = true;
    private bool _waving = false;

    public JBlasting(Juggernaut enemy)
    {
        _enemy = enemy;
    }

    float currentRadius = 0f;


    // Update is called once per frame
    void Update()
    {
        if (_charging)
        {
            _enemy.transform.RotateTowards(_enemy.target.transform, Time.deltaTime * 50, freezeX: true, freezeZ: true);

            if (_chargeTime <= 0f)
            {
                _charging = false;

                Vector3 pos = _enemy.transform.position;

                _enemy.transform.DOMoveY(pos.y + 4, 1.3f).OnStart(() =>
                {
                    _enemy.EnemyAnimator.SetTrigger("Pack");
                }).OnComplete(() =>
                {
                    _enemy.transform.DOMoveY(pos.y, 0.25f).OnComplete(() =>
                    {
                        _waving = true;
                    });
                });
            }
        }


        if (_waving)
        {
            if (currentRadius < _enemy.MaxRadius)
            {
                currentRadius += Time.deltaTime * _enemy.BlastSpeed;
                Draw(currentRadius);
                Damage(currentRadius);
            }
        }


        _chargeTime -= Time.deltaTime;
    }

    public void OnEnter()
    {
        Debug.Log("blast eneter");

        lineRenderer = _enemy.BlastPos.GetComponent<LineRenderer>();

        lineRenderer.positionCount = _enemy.pointsCount + 1;
        currentRadius = 0f;
        Blasted = false;
        _damage = false;
        _charging = true;
        _waving = false;
        _chargeTime = _enemy.ChargeTime;
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
        if (_damage)
        {
            return;
        }

        Collider[] hittingObjects =
            Physics.OverlapSphere(_enemy.BlastPos.position, currentRadius, _enemy.blastMask);

        if (hittingObjects.Length == 0)
        {
            return;
        }

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
                Debug.Log(damageable.transform.name);
            }
        }

        _damage = true;
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