using DG.Tweening;
using UnityEngine;

public class JBlasting : IState
{
    private Juggernaut _enemy;

    private float _chargeTime;

    public bool WavedShocked = false;
    private bool _charging = true;

    private BlastWaveDataSO _blastData;
    private AudioManager _audioManager;

    public JBlasting(Juggernaut enemy, AudioManager audioManager)
    {
        _enemy = enemy;
        _blastData = _enemy.BlastSO;
        _audioManager = audioManager;;
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

                _enemy.transform.DOMoveY(pos.y + 5, 1.1f).OnStart(() =>
                    {
                        _enemy.EnemyAnimator.SetTrigger("Pack");
                        ///////////////////////PLAY SOUND START JUMPING UP
                        _audioManager.PlayOneShot("Boss Go up");
                    })
                    .OnComplete(() =>
                    {
                        ///////////////////////PLAY SOUND FINISHED JUMPING UP
                        _audioManager.PlayOneShot("boss go down hit ground");
                        _enemy.transform.DOMoveY(pos.y, 0.15f).OnComplete(() =>
                        {
                            _enemy.Shield.SetActive(false);

                            BlastWave newWave = GameObject.Instantiate(_enemy._blastWave, _enemy.BlastPos.position,
                                _enemy._blastWave.transform.rotation);
                            newWave.Init(_blastData.pointsCount, _blastData.maxRadius, _blastData.speed,
                                _blastData.startWidth, _blastData.force, _blastData.damage);
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