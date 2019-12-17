using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UICity : MonoBehaviour
{
    public GameObject CityPanel;
    public UIforInventory TraderInventory;

    [SerializeField]
    private GameObject unitsPanel;
    [SerializeField]
    private TextMeshProUGUI cityNameText;
    private City selectedCity;
    private Inventory cityInventory;

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
    }

    public void BuyUnit(int index)
    {
        CreachureStats unit = selectedCity.unitsToBuy[index];
        Inventory.PlayerInventory.Gold -= unit.Cost;

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
        cityInventory = city.gameObject.GetComponent<Inventory>();

        cityInventory.SetInventoryToUI(TraderInventory);

        foreach (CreachureStats unit in city.unitsToBuy)
        {
            GameObject button = Instantiate(Resources.Load("BuyUnitButton"), unitsPanel.transform) as GameObject;
            button.GetComponent<UIBuyUnitBButton>().SetInfo(unit);
            if(Inventory.PlayerInventory.Gold < unit.Cost)
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
