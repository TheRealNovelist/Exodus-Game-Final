using System.Collections;
using System.Collections.Generic;
using EnemySystem;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class JThrowing : IState
{
    private Juggernaut _enemy;
    private Transform _rock;

    private bool _haveRock = false;
    public bool Finished = false;
    private float waitedTime;

    public JThrowing(Juggernaut enemy)
    {
        _enemy = enemy;
    }

    public void Update()
    {
        if (_haveRock)
        {
            _enemy.transform.RotateTowards(_enemy.target.transform, Time.deltaTime * 50, freezeX: true, freezeZ: true);

            waitedTime += Time.deltaTime;

            if (waitedTime >= _enemy.ThrowWaitTime)
            {
                _haveRock = false;
                
                if (_rock.TryGetComponent(out Rigidbody rg))
                {
                    rg.isKinematic = false;
                }

                _rock.transform.DOMove(_enemy.target.position, 0.2f).OnComplete(() =>
                {
                    Finished = true;
                });
            }
        }
    }

    public void OnEnter()
    {
        _haveRock = false;
        Finished = false;
        waitedTime = 0;

        var hittingObjects = GameObject.FindGameObjectsWithTag("Rock");

        if (hittingObjects.Length > 0)
        {
            _rock = hittingObjects[Random.Range(0, hittingObjects.Length - 1)].gameObject.transform;
        }
        else
        {
            Finished = true;
        }

        var enemToRock = _enemy.transform.DOMove(new Vector3(_rock.position.x,_enemy.transform.position.y,_rock.position.z), 3).OnPlay(() =>
        {
            _enemy.transform.LookAt(new Vector3(_rock.position.x, _enemy.transform.position.y, _rock.position.z));
        });

        enemToRock.OnComplete(() =>
        {
            _rock.DOMove(_enemy.ThrowPoint.position, 1.5f);
            if (_rock.TryGetComponent(out Rigidbody rg))
            {
                rg.isKinematic = true;
            }
            
            _haveRock = true;
        });
    }

    public void OnExit()
    {
    }
}