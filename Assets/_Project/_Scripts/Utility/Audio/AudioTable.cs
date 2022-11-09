using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;

#region Custom Data Types
[System.Serializable]
public class AudioFile
{
    public AudioClip clip;
    [Range(0.1f, 1f)] public float volume = 1f;
    [Range(-3f, 3f)] public float pitch = 1f;
}

[System.Serializable]
public class AudioCollection
{
    public string name;
    public List<AudioFile> clips;

    public AudioFile GetRandomClip()
    {
        return clips[Random.Range(0, clips.Count)];
    }
}
#endregion

public enum AudioType
{
    SFX,
    Music
}

[CreateAssetMenu(fileName = "New Audio Object", menuName = "Utility/Audio Table")]
public class AudioTable : ScriptableObject
{
    public AudioMixerGroup audioGroup;
    public AudioType type = AudioType.SFX;

    [SerializeField] private List<AudioCollection> clips;  //creates a protected array of vars for audio clips

    public AudioFile GetAudioClip(string name)
    {
        AudioCollection audioFile = clips.Where(clip => clip.name == name).SingleOrDefault();
        if (audioFile == null)
        {
            return null;
        }

        return audioFile.GetRandomClip();
    }

    private void Awake()
    {
        if (!audioGroup)
        {
            Debug.LogWarning("AudioTable " + name + ": MixerGroup not assigned! Please assign one");
        }
    }
}
