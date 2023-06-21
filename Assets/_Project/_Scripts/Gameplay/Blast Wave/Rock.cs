using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    private bool _activing = false;
    [SerializeField] private float _activeTime = 1.5f;

    public void ActiveRock() => StartCoroutine(WaitToDeactive());

    private void OnCollisionEnter(Collision collision)
    {
        if(!_activing) {return;}
        if (collision.gameObject.CompareTag("Player"))
        {
            //////////////////////PLAY SOUND ROCK HIT PLAYER
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(damage);
            }
        }
    }

    private IEnumerator WaitToDeactive()
    {
        _activing = true;

        yield return new WaitForSeconds(_activeTime);
        
        _activing = false;
    }
    
}
