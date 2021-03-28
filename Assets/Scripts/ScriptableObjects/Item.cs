using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ITEM")]
public class Item : ScriptableObject
{
    public int ID;

    public Sprite icon = null;
    public int Cost = 1;

    public virtual void Use()
    {
        // Use the item
        // Something may happen
    }
}
