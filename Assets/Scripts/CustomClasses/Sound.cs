using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip Clip;
    public VolumeType volumeType;

    [HideInInspector]
    public AudioSource source;
}
