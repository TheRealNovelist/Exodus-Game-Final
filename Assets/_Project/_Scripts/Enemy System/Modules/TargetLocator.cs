using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public enum TargetType
{
    Oxygen,
    Player,
    Any
}

[RequireComponent(typeof(SphereCollider))]
public class TargetLocator : MonoBehaviour
{
    [ReadOnly] public Transform Oxygen;
    [ReadOnly] public Transform Player;
    public Transform Target { get; private set; }

    [Header("Base Settings")]
    [SerializeField] private TargetType preferredTargetType = TargetType.Any;
    [SerializeField] private bool randomAggression;
    [HideIf("randomAggression"), SerializeField] private bool switchOnAggression;
    [ShowIf("randomAggression"), Range (0, 100), SerializeField] private int aggressionChance = 50;

    [Header("Search Settings")] 
    [SerializeField] private bool usePulseSearch = true;
    [SerializeField] private float searchPulseTime = 10f;
    [SerializeField] private float searchRadius = 2f;
    [SerializeField] private float viewDistance = 10f;
    [SerializeField] private LayerMask searchMask;
    
    private SphereCollider _searchCollider;

    private EnemyHealth health => GetComponent<EnemyHealth>();
    
    private List<Transform> _inRangeTargets = new();

    private bool _isAggro;
    private bool _pulse;

    private void Awake()
    {
        GameObject oxygen = GameObject.FindWithTag("Oxygen");

        if (oxygen)
            Oxygen = oxygen.transform;
        
        GameObject player = GameObject.FindWithTag("Player");

        if (player)
            Player = player.transform;

        if (randomAggression)
            switchOnAggression = Random.Range(0, 100) < aggressionChance; 
        
        if (switchOnAggression)
        {
            health.OnDamaged += OnDamagedBy;
        }
    }

    private void Update()
    {
        if ((!Target || !Target.gameObject.activeInHierarchy) && !_pulse && usePulseSearch)
        {
            if (_isAggro)
                _isAggro = false;
            
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
        _pulse = true;
        Target = SelectAnyTarget();
        
        yield return new WaitForSeconds(searchPulseTime);
        
        _pulse = false;
    }
    
    private void OnDestroy()
    {
        if (switchOnAggression)
            health.OnDamaged -= OnDamagedBy;
    }

    private void OnDamagedBy(Transform newTarget)
    {
        if (!_isAggro)
        {
            Target = newTarget;
            _isAggro = true;
        }
    }

    private Transform SelectAnyTarget()
    {
        return _inRangeTargets.Count > 0 ? _inRangeTargets[Random.Range(0, _inRangeTargets.Count - 1)] : null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_inRangeTargets.Contains(collision.transform))
        {
            OnDamagedBy(collision.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(((1<<other.gameObject.layer) & searchMask) != 0)
        {
            _inRangeTargets.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(((1<<other.gameObject.layer) & searchMask) != 0)
        {
            _inRangeTargets.Remove(other.transform);
        }
    }

    private bool CanSeeTarget(Transform target)
    {
        return Physics.Raycast(transform.position, target.position - transform.position, viewDistance);
    }
    
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!_searchCollider)
            _searchCollider = GetComponent<SphereCollider>();
        
        if (_searchCollider)
        {
            _searchCollider.hideFlags = HideFlags.NotEditable;
            _searchCollider.radius = searchRadius;
            _searchCollider.isTrigger = true;
        }
    }
#endif
}
