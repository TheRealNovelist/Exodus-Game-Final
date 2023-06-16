using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EnemySystem;
using Unity.VisualScripting;
using UnityEngine;

public class JBlasting : IState
{
    private Juggernaut _enemy;

    private float _chargeTime;

    public bool Blasted = false;
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
            
            _chargeTime -= Time.deltaTime;
        }

    }

    public void OnEnter()
    {
        Debug.Log("blast eneter");

    }

    public void OnExit()
    {
    }

    void IState.Update()
    {
        Update();
    }

}