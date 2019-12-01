using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UICity : MonoBehaviour
{
    public GameObject CityPanel;

    [SerializeField]
    private GameObject unitsPanel;
    [SerializeField]
    private TextMeshProUGUI cityNameText;
    private City selectedCity;

    public static UICity instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log("More than 1 instance " + this.GetType().ToString());
            Destroy(this);
        }

        unitsPanel.SetActive(false);

        Debug.Log("hello");
    }

    private void Start()
    {
        Debug.Log("hello 1");
    }

    public void BuyUnit(int index)
    {
        CreachureStats unit = selectedCity.unitsToBuy[index];
        GameSettings.instance.PlayerGold -= unit.Cost;

        UnitList.instance.AddUnit(unit);

        selectedCity.unitsToBuy.RemoveAt(index);
    }

    public void SetCityInfo(City city)
    {
        for (int i = 0; i < unitsPanel.transform.childCount; i++)
        {
            Destroy(unitsPanel.transform.GetChild(i).gameObject);
        }

        selectedCity = city;
        cityNameText.text = city.CityName;

        foreach(CreachureStats unit in city.unitsToBuy)
        {
            GameObject button = Instantiate(Resources.Load("BuyUnitButton"), unitsPanel.transform) as GameObject;
            button.GetComponent<UIBuyUnitBButton>().SetInfo(unit);
            if(GameSettings.instance.PlayerGold < unit.Cost)
            {
                button.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void ExitCity()
    {
        gameObject.SetActive(false);
        GlobalMap.instance.GAMEPAUSED = false;
    }
}
