using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class City : MapObject
{
    public string CityName;
    UIGlobalMap map;
    public List<CreachureStats> unitsToBuy;

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

        //UNDONE it's shouldn't be here
        unitsToBuy = new List<CreachureStats>();

        

        for (int i = 0; i <= Random.Range(1, 5); i++)
        {
            CreachureStats unit = new CreachureStats();
            unit.icon = Resources.Load<Sprite>("UnitsIcons/Helmet");
            unitsToBuy.Add(unit);
        }
    }
}
