using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    [HideInInspector]
    public bool isLoadGame = false;

    public string SaveName = "save";
    public FullSaveData save;

    public static SaveManager instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log("More than 1 instance " + this.GetType().ToString());
            Destroy(this);
        }
    }

    public void SaveGame()
    {
        GlobalMap map = GlobalMap.instance;
        save = new FullSaveData(map.mapSizeX,map.mapSizeZ,map.selectedUnit.GetComponent<Unit>(),map.tiles);
        SaveSystem.Save(SaveName, save);
    }

    public void LoadGame()
    {
        save = (FullSaveData)SaveSystem.Load(SaveSystem.SaveDirectory + SaveName + ".save");
        Debug.Log("Loading game");
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            //if(isLoadGame)
            LoadGame();
        }
    }
}
