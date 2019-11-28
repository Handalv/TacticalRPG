using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class City : MapObject
{
    public string CityName;
    UIGlobalMap map;
    List<CreachureStats> unitsToBuy;

    private GameObject UIelement;

    void Awake()
    {
        if (map == null)
            map = UIGlobalMap.instance;
        UIelement = Instantiate(Resources.Load("UnitAmountText"), UIGlobalMap.instance.MapObjectElementsPanel.transform) as GameObject;
        gameObject.GetComponent<MapObject>().GraphicElements.Add(UIelement);
        UIelement.GetComponent<TextMeshProUGUI>().text = CityName;
    }

    private void Start()
    {
        UIelement.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0,2,0));
    }
}
