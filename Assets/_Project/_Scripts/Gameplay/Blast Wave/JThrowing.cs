using System.Collections;
using System.Collections.Generic;
using EnemySystem;
using UnityEngine;
using DG.Tweening;

public class JThrowing : IState
{
    private Juggernaut _enemy;
    private Transform _rock;
    public JThrowing(Juggernaut enemy)
    {
        _enemy = enemy;
    }
    
    public void Update()
    {
       
    }

    public void OnEnter()
    {
        var hittingObjects = GameObject.FindGameObjectsWithTag("Rock");

        if (hittingObjects.Length > 0)
        {
            _rock = hittingObjects[Random.Range(0, hittingObjects.Length - 1)].gameObject.transform;
        }

       var enemToRock = _enemy.transform.DOMove(_rock.position, 5).OnPlay(() =>
        {
            _enemy.transform.LookAt(new Vector3(_rock.position.x, _enemy.transform.position.y, _rock.position.z));
        });

       enemToRock.OnComplete(() => { Debug.Log("Take"); });
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

}
