using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ITEM")]
public class Item : ScriptableObject
{

    public Sprite icon = null;
    public int defaultPrice = 1;

    public virtual void Use()
    {
        // Use the item
        // Something may happen
    }

    //public void RemoveFromInventory(Inventory inventory)
    //{
    //    inventory.Remove(this);
    //}

    public void Trade(Inventory from, Inventory to)
    {
        if(to.Gold >= defaultPrice)
        {
            to.Gold -= defaultPrice;
            from.Gold += defaultPrice;

            to.Items.Add(this);
            from.Items.Remove(this);
        }
    }
}
