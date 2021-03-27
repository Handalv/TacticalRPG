using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    SaveManager saveManager;

    [SerializeField]
    private Slider overallVolumeSlider;
    [SerializeField]
    private Slider musicVolumeSlider;
    [SerializeField]
    private Slider soundVolumeSlider;

    void Start()
    {
        saveManager = SaveManager.instance;
        overallVolumeSlider.value = AudioManager.instance.OverallVolume;
        musicVolumeSlider.value = AudioManager.instance.MusicVolume;
        soundVolumeSlider.value = AudioManager.instance.SoundVolume;
    }

    public void StartGame()
    {
        saveManager.isLoadGame = false;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void LoadGame()
    {
        saveManager.isLoadGame = true;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void OverallVolumeChange()
    {
        AudioManager.instance.OverallVolume = overallVolumeSlider.value;
        AudioManager.instance.currentBackground.source.volume = AudioManager.instance.volumecount(AudioManager.instance.currentBackground.volumeType);
    }
    public void MusicVolumeChange()
    {
        AudioManager.instance.MusicVolume = musicVolumeSlider.value;
        AudioManager.instance.currentBackground.source.volume = AudioManager.instance.volumecount(AudioManager.instance.currentBackground.volumeType);
    }
    public void SoundlVolumeChange()
    {
        AudioManager.instance.SoundVolume = soundVolumeSlider.value;
    }

    public void ApplyOptions()
    {
        PlayerPrefs.SetFloat("OverallVolume", AudioManager.instance.OverallVolume);
        PlayerPrefs.SetFloat("MusicVolume", AudioManager.instance.MusicVolume);
        PlayerPrefs.SetFloat("SoundVolume", AudioManager.instance.SoundVolume);
        PlayerPrefs.Save();
        //AudioManager.instance.currentBackground.source.volume = AudioManager.instance.volumecount(AudioManager.instance.currentBackground.volumeType);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
