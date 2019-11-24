using System.Collections;
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

    public void BattleResultSave()
    {
        save.SavePlayerUnits();
        save.PlayerGold += 20;
        SaveSystem.Save(SaveName, save);
    }

    public void LoadData()
    {
        save = (SaveData)SaveSystem.Load(SaveSystem.SaveDirectory + SaveName + ".save");
    }

    public void LoadGame()
    {
        if (save == null)
            LoadData();

        GameSettings.instance.PlayerGold = save.PlayerGold;


        UnitList.instance.units.Clear();
        for (int i = 0; i < save.UnitIconName.Count; i++)
        {
            //TODO normal constructor
            CreachureStats unit = new CreachureStats();

            unit.MaxHealth = save.UnitHealth[i];
            unit.icon = Resources.Load<Sprite>("UnitsIcons/" + save.UnitIconName[i]);
            UnitList.instance.isOnBattleField[i] = save.UnitIsOnBattleField[i];
            unit.Speed = save.UnitSpeed[i];
            unit.status = (Status)save.UnitStatus[i];
            unit.Damage = save.UnitDamage[i];
            UnitList.instance.BattleFieldIndex[i] = save.UnitBattleIndex[i];

            UnitList.instance.AddUnit(unit);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (save == null)
            LoadData();
        if (level == 1)
        {
            if(isLoadGame)
                LoadGame();
        }
    }
}
