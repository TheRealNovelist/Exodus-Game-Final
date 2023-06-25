using System;
using DG.Tweening;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class SmallBlaster : MonoBehaviour
{
    [SerializeField] private BlastWaveDataSO _blastWaveData;
    [SerializeField] private BlastWave _blastWave;
    [SerializeField] private float gap = 10f;
    [SerializeField] private Transform blastPoint;
    [SerializeField] private Transform head;
    private EnemyHealth _enemyHealth => GetComponent<EnemyHealth>();
    private SmallBlasterSlot _slot;
    [SerializeField] private AudioManager audioManager;
    

    private void Start()
    {
        _enemyHealth.OnDamaged += (df) => { Debug.Log(_enemyHealth.Health); };
        _enemyHealth.OnDeath += _slot.ClearSlot;
        _enemyHealth.OnDeath += () => { Destroy(gameObject); };
        StartCoroutine(WaitToStart());
    }

    public void Init(SmallBlasterSlot slot)
    {
        _slot = slot;
    }

    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(Random.Range(0, 10));
        InvokeRepeating(nameof(Wave), 1, Random.Range(gap, gap + 10));
    }

    private void Wave()
    {
        Vector3 pos = head.transform.position;

        head.transform.DOMoveY(pos.y + 5, 1.1f).OnComplete(() =>
        {
            head.transform.DOMoveY(pos.y, 0.15f).OnComplete(() =>
            {
                //////////SPAWN SMALL WAVE
                audioManager.PlayOneShot("Enemy hit ground");
                BlastWave newWave = GameObject.Instantiate(_blastWave, blastPoint.transform.position,
                    _blastWave.transform.rotation);
                newWave.Init(_blastWaveData.pointsCount, _blastWaveData.maxRadius, _blastWaveData.speed,
                    _blastWaveData.startWidth, _blastWaveData.force, _blastWaveData.damage);
            });
        }).OnStart(() =>
        {
            //////////START JUMPING FOR SMALL THING
            audioManager.PlayOneShot("Sound of enemy go up");
        });
    }

    private void OnDisable()
    {
        _enemyHealth.OnDeath -= _slot.ClearSlot;
        _enemyHealth.OnDeath -= () => { Destroy(gameObject); };
    }
}