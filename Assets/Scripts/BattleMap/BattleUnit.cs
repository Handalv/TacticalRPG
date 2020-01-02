using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleUnit : MapObject
{
    public CreachureStats UnitStats;

    private GameObject UIelement;

    public int UnitHP
    {
        get
        {
            return UnitStats.Health;
        }
        set
        {
            UnitStats.Health = value;
            UIelement.GetComponent<TextMeshProUGUI>().text = value + " hp";
            if (UnitStats.Health <= 0)
            {
                Die();
            }
        }
    }

    //[HideInInspector]
    private int currentActionpoints;
    public int CurrentActionpoints
    {
        get
        {
            return currentActionpoints;
        }
        set
        {
            currentActionpoints = value;
            UIBattleMap.instance.ActionPointsText.text = currentActionpoints + "/" + UnitStats.ActionPoints;
        }
    }
    public List<FightAction> Actions;

    void Awake()
    {
        UIelement = Instantiate(Resources.Load("UnitAmountText"), UIBattleMap.instance.MapObjectElementsPanel.transform) as GameObject;
        UIelement.GetComponent<TextMeshProUGUI>().fontSize = 16;
        UIelement.GetComponent<TextMeshProUGUI>().color = Color.red;
        gameObject.GetComponent<MapObject>().GraphicElements.Add(UIelement);
    }

    void Start()
    {
        UnitHP = UnitHP;
    }

    void Update()
    {
        if (UIelement.GetComponent<TextMeshProUGUI>().gameObject.activeSelf)
        {
            UIelement.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, -1, 0));
        }
    }

    void Die()
    {
        BattleController battleController = BattleController.instance;

        battleController.CurrentBattleOrder[0].UnitStats.ExpCurrent += UnitStats.ExpForKill;

        battleController.BattleOrder.Remove(this);
        if (battleController.CurrentBattleOrder.IndexOf(this) >= 0)
        {
            battleController.RemoveFromOrder(battleController.CurrentBattleOrder.IndexOf(this));
        }

        if (battleController.PlayerBattleList.Contains(this))
        {
            battleController.PlayerBattleList.Remove(this);
            UnitList.instance.units.Remove(UnitStats);
            if (battleController.PlayerBattleList.Count == 0)
            {        
                battleController.Defeat();
            }
        }
        if (battleController.EnemyBattleList.Contains(this))
        {
            //UNDONE Add Loot
            LootGenerator lootGenerator = GetComponent<LootGenerator>();
            lootGenerator.GenerateLoot();
            Inventory loot = UIBattleMap.instance.LootInventory.GetComponent<Inventory>();
            loot.Items.AddRange(lootGenerator.Items);
            loot.Gold += lootGenerator.Gold;

            battleController.EnemyBattleList.Remove(this);
            if (battleController.EnemyBattleList.Count == 0)
            {
                battleController.Victory();
            }
        }

        BattleMap.instance.tiles[tileX, tileZ].mapObjects.Remove(this);

        if (BattleMap.instance.visibleObjects.Contains(this))
        {
            BattleMap.instance.visibleObjects.Remove(this);
        }

        Destroy(this.gameObject);
    }
}
