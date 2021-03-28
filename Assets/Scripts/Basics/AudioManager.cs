using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

[System.Serializable]
public enum VolumeType
{
    Overall = 0,
    Music = 1,
    Sounds = 2
}

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;
    
    [HideInInspector]
    public Sound currentBackground;

    public float OverallVolume;
    public float MusicVolume;
    public float SoundVolume;

    public static AudioManager instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log("More than 1 instance " + this.GetType().ToString());
            Destroy(this);
        }

        OverallVolume = PlayerPrefs.GetFloat("OverallVolume",0.2f);
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        SoundVolume = PlayerPrefs.GetFloat("SoundVolume", 0.75f);

        foreach (Sound s in Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.Clip;
        }
    }

    void Start()
    {
        currentBackground = Play("Background0");
    }

    private void Update()
    {
        //FIX when alttab isPlaying == false 
        if (currentBackground.source.isPlaying == false && currentBackground.source.time == 0)
        {
                String nextBackground = currentBackground.Name;
                nextBackground = "Background" + (Int32.Parse(nextBackground.Replace("Background", "")) + 1);
                Debug.Log(nextBackground);
                Sound s = Array.Find(Sounds, sound => sound.Name == nextBackground);
                if (s != null)
                {
                    currentBackground = Play(nextBackground);
                }
                else
                {
                    Play("Background0");
                }
        }
    }

    public Sound Play(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);
        s.source.volume = volumecount(s.volumeType);
        if (s == null)
        {
            Debug.LogWarning("Didnt find the sound: " + name);
            return null;
        }
        s.source.Play();
        return s;
    }

    public float volumecount(VolumeType type)
    {
        float volume = OverallVolume;

        switch (type){
            case VolumeType.Music:
                volume *= MusicVolume;
                break;
            case VolumeType.Sounds:
                volume *= SoundVolume;
                break;
        }

        return volume;
    }

}
