﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBuyUnitBButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI levelText,
        healthText,
        strengthText,
        agilityText,
        speedText,
        costText;

    [SerializeField]
    private Image icon;

    public void SetInfo(CreachureStats unit)
    {
        levelText.text = "LVL: " + unit.Level.ToString();
        healthText.text = "HP: " + unit.MaxHealth.ToString();
        strengthText.text = "STR: " + unit.Strength.ToString();
        agilityText.text = "AGI: " + unit.Agility.ToString();
        speedText.text = "SPD: " + unit.Speed.ToString();

        costText.text = unit.Cost.ToString();

        icon.sprite = unit.icon;
    }

    public void BuyUnitClick()
    {
        int index = transform.GetSiblingIndex();
        if (UICity.instance.BuyUnit(index))
        {
            Destroy(gameObject);
        }
    }
    
}
