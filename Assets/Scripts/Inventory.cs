using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> Items;

    private int gold = 0;
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            UI.GoldText.text = gold.ToString();
        }
    }
    public int Space = 10;  // Amount of item spaces

    public UIforInventory UI;

    void Start()
    {
        
    }
}
