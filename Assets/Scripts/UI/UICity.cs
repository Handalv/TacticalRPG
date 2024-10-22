﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UICity : MonoBehaviour
{
    public GameObject CityPanel;
    public UIforInventory TraderInventoryUI;

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
    }

    public bool BuyUnit(int index)
    {
        CreachureStats unit = selectedCity.unitsToBuy[index];
        if (Inventory.PlayerInventory.Gold >= unit.Cost)
        {
            Inventory.PlayerInventory.Gold -= unit.Cost;

            UnitList.instance.AddUnit(unit);
            selectedCity.unitsToBuy.RemoveAt(index);
            return true;
        }
        return false;
    }

    public void SetCityInfo(City city)
    {
        for (int i = 0; i < unitsPanel.transform.childCount; i++)
        {
            Destroy(unitsPanel.transform.GetChild(i).gameObject);
        }

        selectedCity = city;

        city.cityInventory.SetInventoryToUI(TraderInventoryUI);

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
        GlobalMap.instance.GAMEPAUSED = false;
        gameObject.SetActive(false);
    }
}
