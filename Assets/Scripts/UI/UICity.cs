using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.UI;

public class UICity : MonoBehaviour
{
    public GameObject cityPanel;
    [SerializeField]
    private TextMeshProUGUI cityName;

    UnitList unitList;

    void Start()
    {
        if (cityPanel==null)
        {
            Debug.Log("CityPanel is null by default");
            cityPanel = this.gameObject;
        }
        unitList = UnitList.instance;
    }

    public void BuyNewUnit()
    {
        int unitcost = 10;
        if ((GameSettings.instance.PlayerGold - unitcost) >= 0)
        {
            GameSettings.instance.PlayerGold -= unitcost;
            PlayerUnitStats unit = new PlayerUnitStats();
            unitList.AddUnit(unit);
        }
    }

    public void SetCityInfo(City city)
    {
        cityName.text = city.CityName;
    }

    public void ExitCity()
    {
        cityPanel.SetActive(false);
        GlobalMap.instance.GAMEPAUSED = false;
    }
}
