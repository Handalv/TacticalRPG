using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class UIforInventory : MonoBehaviour, IDropHandler
{
    public GameObject ItemSlotsParent;
    public TextMeshProUGUI GoldText;
    public InventoryType type;

    public List<GameObject> ItemSlots = new List<GameObject>();

    private Inventory currentInventory;
    public Inventory CurrentInventory
    {
        get
        {
            return currentInventory;
        }
        set
        {
            currentInventory = value;
            InitializeUI(currentInventory);
        }
    }

    public void InitializeUI(Inventory inv)
    {
        currentInventory = inv;
        if (currentInventory != null)
        {
            foreach (GameObject slot in ItemSlots)
            {
                Destroy(slot);
            }
            for (int i = 0; i < currentInventory.Items.Count; i++)
            {
                ItemSlot slot = AddSlot();
                slot.AddItem(currentInventory.Items[i]);
            }
            GoldText.text = "" + currentInventory.Gold;
        }
    }

    public ItemSlot AddSlot()
    {
        GameObject slotGO = Instantiate(Resources.Load("ItemSlot"), ItemSlotsParent.transform) as GameObject;

        ItemSlots.Add(slotGO);

        ItemSlot slot = slotGO.GetComponent<ItemSlot>();
        slot.UI = this;
        slot.parentToReturn = ItemSlotsParent.transform;
        if (SceneManager.sceneCount == 1)
        {
            slot.parentWhenDrag = UIGlobalMap.instance.transform;
        }
        else
        {
            slot.parentWhenDrag = UIBattleMap.instance.transform;
        }

        return slot;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (CurrentInventory.SpaceEnough())
        {
            ItemSlot itemSlot = eventData.pointerDrag.GetComponent<ItemSlot>();
            itemSlot.parentToReturn = ItemSlotsParent.transform;
            itemSlot.UI.ItemSlots.Remove(itemSlot.gameObject);
            itemSlot.UI.CurrentInventory.RemoveItem(itemSlot.item);
            itemSlot.UI = this;
        }
    }
}