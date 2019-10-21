using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitList : MonoBehaviour
{
    UIGlobalMap globalMapUI;

    public List<PlayerUnitStats> units = null;

    public static UnitList instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        UIGlobalMap.instance.InitializeUnits(units);
    }

    public void AddUnit(PlayerUnitStats unit)
    {
        if (globalMapUI == null)
            globalMapUI = UIGlobalMap.instance;
        units.Add(unit);
        globalMapUI.AddUnitOnUI(unit);
    }

}
