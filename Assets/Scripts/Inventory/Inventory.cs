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
        //TEST
        if (UI != null)
        {
            UI.CurrentInventory = this;
        }
        //
        Gold = Gold;
        Type = Type;
    }

    public void SetInventoryToUI(UIforInventory ui)
    {
        UI = ui;
    }

    public bool SpaceEnough()
    {
        if (Space > Items.Count)
        {
            return true;
        }
        return false;
    }

    public bool Trade(Item item, Inventory from, bool isSelling)
    {
        if (SpaceEnough())
        {
            if (isSelling)
            {
                if(Gold >= item.Cost)
                {
                    Gold -= item.Cost;
                    from.Gold += item.Cost;
                }
                else
                {
                    return false;
                }
            }
            Items.Add(item);
            from.Items.Remove(item);
            return true;
        }
        return false;
    }

    //public void AddItem(Item item)
    //{
    //    if (SpaceEnough())
    //    {
    //        Items.Add(item);
    //        //UNDONE Update UI
    //    }
    //}

    //public void RemoveItem(Item item)
    //{
    //    Items.Remove(item);
    //    //UNDONE Update UI
    //}

    void OnLevelWasLoaded(int level)
    {
        //1 - global map
        //2 - battle Map
        if (type == InventoryType.Player)
        {
            if (level == 1)
            {
                UI = UIGlobalMap.instance.PlayerInventory.GetComponent<UIforInventory>();
                UI.CurrentInventory = this;
            }
            if (level == 2)
            {
                UI = UIBattleMap.instance.PlayerInventory.GetComponent<UIforInventory>();
                UI.CurrentInventory = this;
            }
        }
    }
}

public enum InventoryType
{
    Loot, Player, Trader
}
