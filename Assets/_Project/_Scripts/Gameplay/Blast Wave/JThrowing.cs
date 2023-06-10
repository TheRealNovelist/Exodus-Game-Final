using System.Collections;
using System.Collections.Generic;
using EnemySystem;
using UnityEngine;
using DG.Tweening;

public class JThrowing : IState
{
    private Juggernaut _enemy;
    private Transform _rock;

    public bool Thrown = false;
    private bool _throwing = false;
    private float throwingCountDown = 5f;
    private float currentCountDown = 0;

    public JThrowing(Juggernaut enemy)
    {
        _enemy = enemy;
    }

    public void Update()
    {
        if (_rock && !_throwing)
        {
            _enemy.transform.LookAt(new Vector3(_rock.position.x, _enemy.transform.position.y, _rock.position.z));
        }

        if (_throwing)
        {
            /*_enemy.transform.LookAt(new Vector3(_enemy.target.position.x, _enemy.transform.position.y,
                _enemy.target.position.z));*/

            _enemy.transform.RotateTowards(_enemy.target.transform, 6f, true,false,true);
            if (currentCountDown <= 0)
            {
                _throwing = false;
                if (_rock.TryGetComponent(out Rigidbody rigidbody))
                {
                    rigidbody.isKinematic = false;
                }

                _rock.transform.DOMove(_enemy.target.position, 0.5f).OnComplete(() =>
                {
                    Thrown = true;
                    ;
                });

                return;
            }

            currentCountDown -= Time.deltaTime;
        }
    }

    public void OnEnter()
    {
        var hittingObjects = GameObject.FindGameObjectsWithTag("Rock");
        Thrown = false;
        _throwing = false;

        currentCountDown = throwingCountDown;

        if (hittingObjects.Length > 0)
        {
            _rock = hittingObjects[Random.Range(0, hittingObjects.Length - 1)].gameObject.transform;
        }

        _enemy.transform.DOMove(new Vector3(_rock.position.x, _enemy.transform.position.y, _rock.position.z), 5)
            .OnComplete(() =>
            {
                _rock.transform.DOMove(_enemy.ThrowingHand.position, 1).OnComplete(() =>
                {
                    _throwing = true;
                    if (_rock.TryGetComponent(out Rigidbody rigidbody))
                    {
                        rigidbody.isKinematic = true;
                    }
                });
            });
    }


    public void OnExit()
    {
    }
}