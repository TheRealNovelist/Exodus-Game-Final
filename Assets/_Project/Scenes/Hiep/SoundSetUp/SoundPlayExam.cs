using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayExam : MonoBehaviour
{
    public AudioManager audioManager;

    [SerializeField] private string shootingSoundName;
    [SerializeField] private string reloadingSoundName;
    [SerializeField] private string runningSoundName;
    [SerializeField] private string gunEmptyClickSoundName;

    public void PlayShootingSound()
    {
        audioManager.PlayOneShot(shootingSoundName);
    }

    public void PlayReloadingSound()
    {
        audioManager.PlayOneShot(reloadingSoundName);
    }

    public void PlayRunningSound()
    {
        audioManager.PlayOneShot(runningSoundName);
    }

    public void PlayGunEmptyClickSound()
    {
        audioManager.PlayOneShot(gunEmptyClickSoundName);
    }
}