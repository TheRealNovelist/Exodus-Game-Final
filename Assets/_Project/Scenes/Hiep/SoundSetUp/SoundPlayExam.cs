using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundPlayExam : MonoBehaviour
{
    private AudioManager audioManager; //tạo một reference của AudioManager, xác nhận lại ở awake

    //âm thanh cần có điều kiện để chạy, nếu không cần điều kiện có thì không cần dòng ở dưới
    [SerializeField] private bool playShootingSound;

    //cần nạp tên của sound giống với mục Audio Name trong Audio Manager script
    [SerializeField] private string shootingSoundName;
    [SerializeField] private bool playReloadingSound;
    [SerializeField] private string reloadingSoundName;
    [SerializeField] private bool isRunning = false;
    [SerializeField] private string runningSoundName;

    private bool isRunningSoundPlaying = false;
    
    private void Awake()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        //các logic ở dưới dùng cho các sound ngắn và không lặp lại
        //như là tiếng sún, sound effect...
        if (playShootingSound)
        {
            audioManager.PlayOneShot(shootingSoundName);
            playShootingSound = false;
        }

        if (playReloadingSound)
        {
            audioManager.PlayOneShot(reloadingSoundName);
            playReloadingSound = false;
        }

        //các logic ở dưới dùng cho các sound cần loop, có thể dừng giữa trừng và bắt đầu lại
        //ví dụ như tiếng di chuyển, tiếng thở,...
        if (isRunning)
        { 
            Debug.Log("here");
            audioManager.Play(runningSoundName, true);
        }
        else
        {
            Debug.Log("should not run");
            audioManager.Stop();
        }
    }
}
