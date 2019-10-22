﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitList : MonoBehaviour
{
    UIGlobalMap globalMapUI;

    public List<PlayerUnitStats> units = null;

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
            UIGlobalMap.instance.InitializeUnits(units);
        }
    }

    public void AddUnit(PlayerUnitStats unit)
    {
        if (globalMapUI == null)
            globalMapUI = UIGlobalMap.instance;
        units.Add(unit);
        globalMapUI.AddUnitOnUI(unit);
    }

}