﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    [HideInInspector]
    public bool isLoadGame = false;

    public string SaveName = "save";
    public SaveData save;

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

        save = new SaveData(
            map.mapSizeX, map.mapSizeZ,
            map.selectedUnit.GetComponent<Unit>(),
            map.tiles,
            UnitList.instance,
            GameSettings.instance.PlayerGold
            );

        SaveSystem.Save(SaveName, save);
    }

    public void LoadGame()
    {
        Debug.Log("Loading game");
        save = (SaveData)SaveSystem.Load(SaveSystem.SaveDirectory + SaveName + ".save");

        GameSettings.instance.PlayerGold = save.PlayerGold;

        // foreach unit health  
        // i prob need to name units
        UnitList.instance.units.Clear();
        int index=0;
        foreach (string icon in save.UnitIconName)
        {
            //TODO normal constructor
            PlayerUnitStats unit = new PlayerUnitStats();

            unit.MaxHealth = save.UnitHealth[index];
            unit.icon = (Sprite)Resources.Load("UnitsIcons/" + save.UnitIconName[index]);
            Debug.Log("UnitsIcons/" + save.UnitIconName[index]);
            unit.isOnBattlefield = save.UnitIsOnBattleField[index];
            unit.Speed = save.UnitSpeed[index];
            unit.status = (Status)save.UnitStatus[index];
            unit.Damage = save.UnitDamage[index];
            unit.battlefieldIndex = save.UnitBattleIndex[index];

            UnitList.instance.AddUnit(unit);

            index++;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            if(isLoadGame)
                LoadGame();
        }
    }
}
