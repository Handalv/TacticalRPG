using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class City : MapObject
{
    public string CityName;
    public List<CreachureStats> unitsToBuy = new List<CreachureStats>();
    public Inventory cityInventory;

    [SerializeField]
    private float reinforceCD = 300;
    private float currentReinforceCD;

    private GameObject UICityNameText;
    UIGlobalMap map;

    void Awake()
    {
        if (map == null)
            map = UIGlobalMap.instance;
        UICityNameText = Instantiate(Resources.Load("UnitAmountText"), UIGlobalMap.instance.MapObjectElementsPanel.transform) as GameObject;
        gameObject.GetComponent<MapObject>().GraphicElements.Add(UICityNameText);
        UICityNameText.GetComponent<TextMeshProUGUI>().text = CityName;
    }

    void Start()
    {
        UICityNameText.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0,2,0));

        //UNDONE it's shouldn't be here
        //unitsToBuy = new List<CreachureStats>();

        ReforceUnits();
        ReforceInventory();
        currentReinforceCD = reinforceCD;
    }

    void Update()
    {
        currentReinforceCD -= Time.deltaTime;
        if (currentReinforceCD <= 0)
        {
            ReforceUnits();
            ReforceInventory();
            currentReinforceCD = reinforceCD;
        }
    }

    private void ReforceUnits()
    {
        for (int i = 0; i <= Random.Range(1, 5); i++)
        {
            CreachureStats unit = new CreachureStats();
            unit.icon = Resources.Load<Sprite>("UnitsIcons/Helmet");
            unitsToBuy.Add(unit);
        }
    }

    private void ReforceInventory()
    {
        cityInventory.Gold = 1000;
        cityInventory.Items.Clear();
        int n = Random.Range(10, 30);//30<100
        for (int i = 0; i < n; i++)
        {
            cityInventory.Items.Add(GameSettings.instance.ItemList[Random.Range(0, GameSettings.instance.ItemList.Count)]);
        }
        // Update TradeUI if city is open
        // or better use game time instread of app time
    }
}
