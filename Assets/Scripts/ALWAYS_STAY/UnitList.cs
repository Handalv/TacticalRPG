using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitList : MonoBehaviour
{
    UIGlobalMap globalMapUI;

    public List<CreachureStats> units;
    public List<bool> isOnBattleField;
    public List<int> BattleFieldIndex;

    public static UnitList instance; //PlayerUnitList
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log("More than 1 instance " + this.GetType().ToString());
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    void OnLevelWasLoaded(int level)
    {
        //1 - global map
        if (level == 1)
        {
            if(SaveManager.instance.isLoadGame == false)
                UIGlobalMap.instance.InitializeUnits();
        }
    }

    public void AddUnit(CreachureStats unit)
    {
        if (globalMapUI == null)
            globalMapUI = UIGlobalMap.instance;
        units.Add(unit);
        globalMapUI.AddUnitOnUI(units.Count-1, units[units.Count - 1].icon, isOnBattleField[units.Count - 1], BattleFieldIndex[units.Count - 1]);
    }

}
