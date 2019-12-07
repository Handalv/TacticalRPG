using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory PlayerInventory;

    public UIforInventory UI;

    public List<Item> Items = new List<Item>();

    [SerializeField]
    private int gold = 0;
    public int Space = 10;  // Amount of item spaces

    [SerializeField]
    private InventoryType type;
    public InventoryType Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
            if(type == InventoryType.Player)
            {
                PlayerInventory = this;
                DontDestroyOnLoad(this);
            }
        }
    }

    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            if (UI != null)
            {
                UI.GoldText.text = gold.ToString();
            }
        }
    }

    void Start()
    {
        Gold = Gold;
        Type = Type;
    }

    public void SetInventoryToUI(UIforInventory ui)
    {
        UI = ui;
    }

    public bool SpaceEnough()
    {
        if (Items.Count< Items.Count)
        {
            return true;
        }
        return false;
    }

        public bool Add(Item item)
    {
        if (Space > Items.Count)
        {
            Items.Add(item);
            //UNDONE Update UI
            return true;
        }
        return false;
    }

    public void RemoveItem(Item item)
    {
        Items.Remove(item);
        //UNDONE Update UI
    }

    void OnLevelWasLoaded(int level)
    {
        //1 - global map
        //2 - battle Map
        if (level == 1)
        {
            UI = UIGlobalMap.instance.PlayerInventory;
            UI.CurrentInventory = this;
        }
        if (level == 2)
        {
            UI = UIBattleMap.instance.PlayerInventory;
            UI.CurrentInventory = this;
        }
    }
}

//[System.Serializable]
public enum InventoryType
{
    Loot, Player, Trader
}
