using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    public AudioManager audioManager;
    private Shop shop;

    void Start()
    {
        shop.OnBackgroundMusicChange += PlayBackBroundMusic;
    }

    void OnDestroy()
    {
        shop.OnBackgroundMusicChange -= PlayBackBroundMusic;
    }

    void PlayBackBroundMusic(bool toggle)
    {
        if (!toggle)
        {
            audioManager.Stop();
            audioManager.Play("BackGround", true);
        }
        // Ngược lại, chơi nhạc nền ShopBackGround
        else
        {
            audioManager.Stop();
            audioManager.Play("ShopBackGround", true);
        }
    }
}