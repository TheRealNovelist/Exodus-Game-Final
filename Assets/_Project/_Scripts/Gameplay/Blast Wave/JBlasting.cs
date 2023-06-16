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

    public bool WavedShocked = false;
    private bool _charging = true;

    public JBlasting(Juggernaut enemy)
    {
        _enemy = enemy;
    }

    // Update is called once per frame
    void Update()
    {
        if (_charging)
        {
            _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, new Vector3(
                _enemy.target.transform.position.x, _enemy.transform.position.y,
                _enemy.target.transform.position.z), 3 * Time.deltaTime);
            _enemy.transform.RotateTowards(_enemy.target.transform, Time.deltaTime * 50, freezeX: true, freezeZ: true);

            if (_chargeTime <= 0f)
            {
                _charging = false;

                Vector3 pos = _enemy.transform.position;

                _enemy.transform.DOMoveY(pos.y + 5, 1.1f).OnStart(() => { _enemy.EnemyAnimator.SetTrigger("Pack"); })
                    .OnComplete(() =>
                    {
                        _enemy.transform.DOMoveY(pos.y, 0.15f).OnComplete(() =>
                        {
                            _enemy.Shield.SetActive(false);

                            BlastWave newWave = GameObject.Instantiate(_enemy._blastWave, _enemy.BlastPos.position,
                                _enemy._blastWave.transform.rotation);
                            newWave.Init(25, 30, 20, 1f, 20, 10);
                            WavedShocked = true;
                        });
                    });
            }

            _chargeTime -= Time.deltaTime;
        }
    }

    public void OnEnter()
    {
        Debug.Log("blast eneter");
        _charging = true;
        _chargeTime = _enemy.ChargeTime;
        WavedShocked = false;
        _enemy.Shield.SetActive(true);


    }

    public void OnExit()
    {
    }

    void IState.Update()
    {
        Update();
    }
}