using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType
{
    Any,
    Oxygen,
    Player
}

public class TargetLocator : MonoBehaviour
{
    public static Transform Oxygen => GameObject.FindWithTag("Oxygen").transform;
    public static Transform Player => GameObject.FindWithTag("Player").transform;
    public Transform Target { get; private set; }

    [Header("Base Settings")]
    [SerializeField] private TargetType preferredTargetType = TargetType.Any;
    [SerializeField] private bool switchOnAggression;

    [Header("Search Settings")] 
    [SerializeField] private bool usePulseSearch = true;
    [SerializeField] private float searchPulseTime = 10f;
    [SerializeField] private float searchRadius = 2f;
    [SerializeField] private int maxTargetCheck = 10;
    [SerializeField] private LayerMask searchMask;

    private EnemyHealth health => GetComponent<EnemyHealth>();

    private bool _isPrioritizing;

    private float _timer; 
    
    private void Awake()
    {
        if (switchOnAggression)
        {
            health.OnDamaged += SetTarget;
        }

        if (preferredTargetType == TargetType.Any)
        {
            searchRadius = 100f;
        }
        
        SearchTarget();
    }

    private void Update()
    {
        //If target is no longer found
        if (!Target || !Target.gameObject.activeInHierarchy)
        {
            SearchTarget();
            _timer = 0f;

            if (_isPrioritizing)
                _isPrioritizing = false;
            
            return;
        }

        if (!usePulseSearch || _isPrioritizing) return;

        if (_timer < searchPulseTime)
        {
            _timer += Time.deltaTime;
            return;
        }

        _timer = 0f;
        SearchTarget();
    }

    private void OnDestroy()
    {
        health.OnDamaged -= SetTarget;
    }

    private void SetTarget(Transform newTarget)
    {
        if (_isPrioritizing) return;

        _isPrioritizing = true;
        Target = newTarget;
    }

    private void SearchTarget()
    {
        switch (preferredTargetType)
        {
            case TargetType.Oxygen:
                if (Oxygen != null)
                    Target = Oxygen;
                else
                    Debug.Log("[TargetLocator] Oxygen not found");
                break;
            case TargetType.Player:
                if (Player != null)
                    Target = Player;
                else
                    Debug.Log("[TargetLocator] Player not found");
                break;
        }

        if (SearchAnyTarget(out Transform target))
        {
            Target = target;
        }
    }
    
    private bool SearchAnyTarget(out Transform target)
    {
        Collider[] colliders = new Collider[maxTargetCheck];
        int collidersCount = Physics.OverlapSphereNonAlloc(transform.position, searchRadius, colliders, searchMask);

        if (collidersCount > 0)
        {
            foreach (var checkCollider in colliders)
            {
                //Check if anything in between the collider
                if (Physics.Raycast(transform.position, checkCollider.transform.position - transform.position,
                        searchRadius))
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}
