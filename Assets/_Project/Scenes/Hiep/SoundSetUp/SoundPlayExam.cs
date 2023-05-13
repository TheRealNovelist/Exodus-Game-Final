using UnityEngine;

public class SoundPlayExam : MonoBehaviour
{
    [SerializeField] private string shootingSoundName;
    [SerializeField] private string reloadingSoundName;
    [SerializeField] private string runningSoundName;
    [SerializeField] private string gunEmptyClickingSoundName;

    [SerializeField] private AudioManager audioManager;

    private bool isShooting;
    private bool isReloading;
    private bool isRunning;
    private bool isGunEmptyClicking;
    private bool runningSoundPlaying;
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isShooting = true;
            PlayShootingSound();
        }
        if (Input.GetMouseButtonUp(1))
        {
            isShooting = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            isGunEmptyClicking = true;
            
            PlayGunEmptyClickingSound();
        }
        if (Input.GetMouseButtonUp(0))
        {
            isGunEmptyClicking = false;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            isReloading = true;
            PlayReloadingSound();
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            isReloading = false;
        }
        

        if (isRunning && !runningSoundPlaying)
        {
            PlayRunningSound();
            runningSoundPlaying = true;
        }
        else if (!isRunning && runningSoundPlaying)
        {
            StopRunningSound();
            runningSoundPlaying = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    private void PlayShootingSound()
    {
        if (audioManager)
        {
            audioManager.PlayOneShot(shootingSoundName);
        }
    }

    private void PlayReloadingSound()
    {
        if (audioManager)
        {
            audioManager.PlayOneShot(reloadingSoundName);
        }
    }

    private void PlayRunningSound()
    {
        if (audioManager)
        {
            audioManager.Play(runningSoundName, true);
        }
    }

    private void StopRunningSound()
    {
        if (audioManager)
        {
            audioManager.Stop();
        }
    }

    private void PlayGunEmptyClickingSound()
    {
        if (audioManager)
        {
            audioManager.PlayOneShot(gunEmptyClickingSoundName);
        }
    }
}
