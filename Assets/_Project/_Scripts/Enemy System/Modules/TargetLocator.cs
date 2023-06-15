using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType
{
    Oxygen,
    Player,
    Any
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
    [SerializeField] private float viewDistance = 10f;
    [SerializeField] private int maxTargetCheck = 10;
    [SerializeField] private LayerMask searchMask;

    private EnemyHealth health => GetComponent<EnemyHealth>();

    private bool _isPrioritizing;
    private bool _pulse;

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
        switch (type)
        {
            case TargetType.Oxygen:
                if (Oxygen != null && CanSeeTarget(Oxygen.transform))
                    Target = Oxygen;
                else
                {
                    Debug.Log("[TargetLocator] Oxygen not found");
                    SearchTarget(TargetType.Player);
                }
                break;
            case TargetType.Player:
                if (Player != null && CanSeeTarget(Player.transform))
                    Target = Player;
                else
                {
                    Debug.Log("[TargetLocator] Player not found");
                    SearchTarget(TargetType.Any);
                }
                break;
            case TargetType.Any:
                StartCoroutine(FindTarget());
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
        
        if (!Target)
            _pulse = false;
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

    private bool SearchAnyTarget(out Transform target)
    {
        Collider[] colliders = new Collider[maxTargetCheck];
        int collidersCount = Physics.OverlapSphereNonAlloc(transform.position, searchRadius, colliders, searchMask);

        if (collidersCount > 0)
        {
            foreach (var checkCollider in colliders)
            {
                //Check if anything in between the collider
                if (CanSeeTarget(checkCollider.transform))
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
        if (collision.gameObject.layer == searchMask)
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
