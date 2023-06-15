using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum TargetType
{
    Oxygen,
    Player,
    Any
}

public class TargetLocator : MonoBehaviour
{
    [ReadOnly] public Transform Oxygen;
    [ReadOnly] public Transform Player;
    public Transform Target { get; private set; }

    [Header("Base Settings")]
    [SerializeField] private TargetType preferredTargetType = TargetType.Any;
    [SerializeField] private bool switchOnAggression;

    [Header("Search Settings")] 
    [SerializeField] private bool usePulseSearch = true;
    [SerializeField] private float searchPulseTime = 10f;
    [SerializeField] private float searchRadius = 2f;
    [SerializeField] private float viewDistance = 10f;
    [SerializeField] private int maxTargetCheck = 10;
    [SerializeField] private LayerMask searchMask;

    private EnemyHealth health => GetComponent<EnemyHealth>();

    private bool _isPrioritizing;
    private bool _pulse;

    private void Awake()
    {
        GameObject oxygen = GameObject.FindWithTag("Oxygen");

        if (oxygen)
            Oxygen = oxygen.transform;
        
        GameObject player = GameObject.FindWithTag("Player");

        if (player)
            Player = player.transform;
        
        if (switchOnAggression)
        {
            health.OnDamaged += SetTarget;
        }
    }

    private void Update()
    {
        //If target is no longer found
        if ((!Target || !Target.gameObject.activeInHierarchy) && !_pulse && usePulseSearch)
        {
            if (_isPrioritizing)
                _isPrioritizing = false;
            
            SearchTarget(preferredTargetType);
        }
    }

    private void SearchTarget(TargetType type)
    {
        while (true)
        {
            switch (type)
            {
                case TargetType.Oxygen:
                    if (Oxygen != null && CanSeeTarget(Oxygen.transform))
                        Target = Oxygen;
                    else
                    {
                        Debug.Log("[TargetLocator] Oxygen not found");
                        type = TargetType.Player;
                        continue;
                    }

                    break;
                case TargetType.Player:
                    if (Player != null && CanSeeTarget(Player.transform))
                        Target = Player;
                    else
                    {
                        Debug.Log("[TargetLocator] Player not found");
                        type = TargetType.Any;
                        continue;
                    }

                    break;
                case TargetType.Any:
                    StartCoroutine(FindTarget());
                    break;
            }

            break;
        }
    }

    private IEnumerator FindTarget()
    {
        WaitForSeconds wait = new(searchPulseTime);
        
        _pulse = true;
        SearchAnyTarget(out Transform target);
        Target = target;
        
        yield return wait;
        
        _pulse = false;
    }
    
    private void OnDestroy()
    {
        health.OnDamaged -= SetTarget;
    }

    private void SetTarget(Transform newTarget)
    {
        if (newTarget == Player || !_isPrioritizing)
        {
            _isPrioritizing = true;
            Target = newTarget;
        }
    }

    private bool SearchAnyTarget(out Transform target)
    {
        Collider[] colliders = new Collider[maxTargetCheck];
        if (Physics.OverlapSphereNonAlloc(transform.position, searchRadius, colliders, searchMask) > 0)
        {
            foreach (var checkCollider in colliders)
            {
                //Check if anything in between the collider
                if (!CanSeeTarget(checkCollider.transform))
                {
                    continue;
                }

                if (checkCollider.TryGetComponent(out IDamageable damageable))
                {
                    target = damageable.transform;
                    return true;
                }
            }
        }
        
        target = null;
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(((1<<collision.gameObject.layer) & searchMask) != 0)
        {
            SetTarget(collision.transform);
        }
    }

    private bool CanSeeTarget(Transform target)
    {
        return Physics.Raycast(transform.position, target.position - transform.position, viewDistance);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}
