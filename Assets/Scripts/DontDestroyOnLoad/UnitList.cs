using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitList : MonoBehaviour
{
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
        FindObjectOfType<UIGlobalMap>().InitializeUnits(units);
    }

    public void AddUnit(PlayerUnitStats unit)
    {
        units.Add(unit);
    }

}
