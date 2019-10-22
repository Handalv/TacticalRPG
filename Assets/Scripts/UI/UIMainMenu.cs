using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    SaveManager saveManager;

    void Start()
    {
        saveManager = SaveManager.instance;
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

    public void ExitGame()
    {
        Application.Quit();
    }
}
