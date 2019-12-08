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
            InitializeNewInventory(currentInventory);
        }
    }

    public void InitializeNewInventory(Inventory inv)
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
                CreateItemSlot(currentInventory.Items[i]);
            }
            GoldText.text = "" + currentInventory.Gold;
        }
    }

    public void CreateItemSlot(Item item = null)
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

        if (item != null)
        {
            slot.AddItem(item);
        }
        //return slot;
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemSlot itemSlot = eventData.pointerDrag.GetComponent<ItemSlot>();
        Inventory inventoryFrom = itemSlot.UI.CurrentInventory;
        bool isSelling = false;
        if (CurrentInventory.Type == InventoryType.Trader || inventoryFrom.Type == InventoryType.Trader)
        {
            isSelling = true;
        }
        if (CurrentInventory.Trade(itemSlot.item,inventoryFrom,isSelling))
        {
            itemSlot.UI.ItemSlots.Remove(itemSlot.gameObject);
            ItemSlots.Add(itemSlot.gameObject);
            itemSlot.parentToReturn = ItemSlotsParent.transform;
            itemSlot.UI = this;
        }
    }
}