using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MapObject
{
    UIGlobalMap map;
    public string CityName;
    //List<PlayerUnitStats> unitsToBuy;

    void Awake()
    {
        if (map == null)
            map = UIGlobalMap.instance;
    }
}
