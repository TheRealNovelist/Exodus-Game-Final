using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioTable table;

    [Header("Setting")]
    [SerializeField] private bool loop;
    private void Awake()
    {
        if (!source)
        {
            Debug.LogWarning("AudioController " + gameObject.name + ": Source not found! Adding AudioSource component.");
            gameObject.AddComponent<AudioSource>();
        }

        source.outputAudioMixerGroup = table.audioGroup;
    }

    public void PlaySoundEffect(string name)
    {
        AudioFile file = table.GetAudioClip(name);
        if (file == null)
        {
            Debug.LogWarning("AudioController " + gameObject.name + ": Sound [" + name + "] not found!");
            return;
        }

        source.PlayOneShot(file.clip);
    }
}
