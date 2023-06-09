using System.Collections;
using System.Collections.Generic;
using EnemySystem;
using UnityEngine;

public class JBlasting : IState
{
    private LineRenderer lineRenderer;

    private Juggernaut _enemy;
    private LineRenderer _lineRenderer;
    

    private float _chargeTime;

    public bool Blasted = false;

    public JBlasting(Juggernaut enemy)
    {
        _enemy = enemy;
    }
    
    float currentRadius = 0f;

   
    // Update is called once per frame
    void Update()
    {
        if (_chargeTime <= 0f)
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
        
        Blasted = false;
        
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
        Debug.Log("damge");
        Collider[] hittingObjects = Physics.OverlapSphere(_enemy.BlastPos.position, currentRadius, _enemy.blastMask);

        if (hittingObjects.Length == 0)
        {
            return;
        }

        for (int i = 0; i < hittingObjects.Length ; i++)
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
        
        Blasted = false;
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
    }


}
