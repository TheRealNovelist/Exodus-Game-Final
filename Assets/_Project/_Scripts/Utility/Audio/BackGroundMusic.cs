using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    public AudioManager audioManager;
    private Shop shop;

    void Start()
    {
        audioManager.Play("BackGround", true);
    }
    public void PlayBackBroundMusic(bool toggle)
    {
        if (!toggle)
        {
            audioManager.Stop();
            audioManager.Play("BackGround", true);
        }
        else
        {
            audioManager.Stop();
            audioManager.Play("ShopBackGround", true);
        }
    }
}