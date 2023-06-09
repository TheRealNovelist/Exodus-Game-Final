using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossPillarController : MonoBehaviour
{
    [SerializeField] private List<Pillar> pillars;
    [SerializeField] private GameObject shield;

    private void Awake()
    {
        shield.gameObject.SetActive(false);
        
        foreach (var pillar in pillars)
        {
            pillar.gameObject.SetActive(false);
        }
    }

    public void StartAttack()
    {
        shield.gameObject.SetActive(true);
        
        foreach (var pillar in pillars)
        {
            pillar.gameObject.SetActive(true);
        }
    }

    public bool IsAllPillarDestroyed()
    {
        return pillars.All(pillar => !pillar.gameObject.activeInHierarchy);
    }

    public void StopAttack()
    {
        shield.gameObject.SetActive(false);
        
        foreach (var pillar in pillars)
        {
            if (pillar.gameObject.activeInHierarchy) 
                pillar.StartDisablePillar();
        }
    }
}
