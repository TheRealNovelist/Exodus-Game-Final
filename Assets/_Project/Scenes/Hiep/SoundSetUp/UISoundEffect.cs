using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundEffect : MonoBehaviour
{
    public AudioManager AudioManager;
    public void PlayEffect(string audioName)
    {
        AudioManager.PlayOneShot(audioName);
    }
}
