using System.Linq;
using DG.Tweening;
using UnityEngine;

public class JSpawning : IState
{
    private Juggernaut _enemy;
    public bool Spawned = false;

    public JSpawning(Juggernaut enemy)
    {
        _enemy = enemy;
    }

    public void OnEnter()
    {
        Spawned = false;

        Debug.Log("spawn");

        Vector3 pos = _enemy.transform.position;
        Vector3 rot = _enemy.transform.eulerAngles;
        _enemy.transform.DOMoveY(pos.y + 5, 1f).OnPlay(() =>
        {
            _enemy.transform.DORotate(rot + new Vector3(0, 180, 0), 1);
        }).OnComplete(() =>
            {
                  rot = _enemy.transform.eulerAngles;
                Spawn();
                _enemy.transform.DOMoveY(pos.y, 3f).OnPlay(() =>
                {
                    _enemy.transform.DORotate(rot + new Vector3(0, 180, 0), 3f);
                }).OnComplete(() => { Spawned = true; });
            }
        );
    }

    private void Spawn()
    {
        var slots = _enemy.BlasterSlots;

        var existing = slots.Count(x => !x.SlotAvailable);

        if (existing >= _enemy.MaxSmallBlaster)
        {
            return;
        }

        var slotLeft = _enemy.MaxSmallBlaster - existing;
        var avaiable = slots.Where(x => x.SlotAvailable).OrderBy(x => Random.value).ToList();
        for (int i = 0; i < slotLeft; i++)
        {
            SmallBlaster newBlaster =
                SmallBlaster.Instantiate(_enemy.SmallBlaster, avaiable[i].transform.position, Quaternion.identity);

            avaiable[i].Init(newBlaster);
            newBlaster.Init(avaiable[i]);
        }
    }

    public void OnExit()
    {
    }

    void IState.Update()
    {
    }
}