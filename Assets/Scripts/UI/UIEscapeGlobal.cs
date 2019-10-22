using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEscapeGlobal : MonoBehaviour
{
    SaveManager saveManager;

    void Start()
    {
        saveManager = SaveManager.instance;
    }

    public void Close()
    {
        GlobalMap.instance.GAMEPAUSED = false;
        gameObject.SetActive(false);
    }

    public void Save()
    {
        saveManager.SaveGame();
    }

    public void Quit()
    {
        //TEST DONT SAVE ON EXIT
        //saveManager.SaveGame();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
