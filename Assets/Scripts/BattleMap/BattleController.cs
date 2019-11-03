using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public bool isPlayerTurn;

    public List<BattleUnit> BattleOrder = null;
    public List<BattleUnit> CurrentBattleOrder = null;

    public List<BattleUnit> EnemyBattleList = null;
    public List<BattleUnit> PlayerBattleList = null;

    UnitList playerUnits;
    public BattleMap map;

    public static BattleController instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log("More than 1 instance " + this.GetType().ToString());
            Destroy(this);
        }
        playerUnits = UnitList.instance;
    }

    public void SpawnPlayerUnits()
    {
        int offsetX = (map.mapSizeX / 2) - (map.mapSizeX / 4);
        int offsetZ = 7;
        for (int i = 0; i < playerUnits.units.Count; i++)
        {
            if (playerUnits.isOnBattleField[i])
            {
                CreachureStats creachure = playerUnits.units[i];
                GameObject spawned = GameObject.Instantiate(Resources.Load("BattlePlayerUnit")) as GameObject;

                BattleUnit battleUnit = spawned.GetComponent<BattleUnit>();
                PlayerBattleList.Add(battleUnit);
                BattleOrder.Add(battleUnit);

                int tileX = playerUnits.BattleFieldIndex[i] / 6 + offsetX;
                int tileZ = playerUnits.BattleFieldIndex[i] % 6 + offsetZ;

                battleUnit.tileX = tileX;
                battleUnit.tileZ = tileZ;
                battleUnit.UnitStats = creachure;
                battleUnit.visionRange = creachure.VisionRange;
                battleUnit.CurrenActionpoints = creachure.ActionPoints;

                spawned.transform.position = BattleMap.ConvertTileCoordToWorld(battleUnit.tileX, battleUnit.tileZ);
                map.tiles[tileX, tileZ].mapObjects.Add(battleUnit);
                map.visibleObjects.Add(battleUnit);
            }
        }
    }

    public void SpawnEnemyUnits()
    {
        int offsetX = (map.mapSizeX / 2) + (map.mapSizeX / 4);
        int x = 0;
        int z = 0;
        foreach (UnitPreset preset in GameSettings.instance.Enemies)
        {
            CreachureStats creachure = new CreachureStats(preset); 
            GameObject spawned = GameObject.Instantiate(Resources.Load("EnemyBattle")) as GameObject;

            BattleUnit battleUnit = spawned.GetComponent<BattleUnit>();
            EnemyBattleList.Add(battleUnit);
            BattleOrder.Add(battleUnit);

            //UNDONE Check to melee go first
            int offsetZ = (z / 2);
            if (z % 2 != 0)
            {
                offsetZ++;
                offsetZ *= -1;
            }
            int tileZ = (map.mapSizeZ / 2) - 1 + offsetZ;
            int tileX = offsetX + x;
            z++;
            if (z == 8)
            {
                z = 0;
                x++;
            }

            battleUnit.tileX = tileX;
            battleUnit.tileZ = tileZ;
            battleUnit.UnitStats = creachure;
            battleUnit.visionRange = creachure.VisionRange;
            battleUnit.CurrenActionpoints = creachure.ActionPoints;

            spawned.transform.position = BattleMap.ConvertTileCoordToWorld(battleUnit.tileX, battleUnit.tileZ);
            map.tiles[tileX, tileZ].mapObjects.Add(battleUnit);
        }
    }

    // Sorting Battle order by units speed and set current order for new wave of turns
    public void InitializeOrder()
    {
        BattleOrder.Sort(delegate (BattleUnit x, BattleUnit y) {
            return y.UnitStats.Speed.CompareTo(x.UnitStats.Speed);
        });
        CurrentBattleOrder = new List<BattleUnit>(BattleOrder);
        foreach (BattleUnit unit in CurrentBattleOrder)
        {
            GameObject go = new GameObject();
            Image image = go.AddComponent<Image>();
            image.sprite = unit.UnitStats.icon;
            go.transform.SetParent(UIBattleMap.instance.BattleOrderPanel.transform);

            unit.CurrenActionpoints = unit.UnitStats.ActionPoints;
        }
        isPlayerTurn = PlayerBattleList.Contains(CurrentBattleOrder[0]);
    }

    public void RemoveFromOrder()
    {
        CurrentBattleOrder[0].CurrentPath = null;
        //Destroy(UIBattleMap.instance.BattleOrderPanel.transform.GetChild(0).gameObject);
        CurrentBattleOrder.RemoveAt(0);
        if (CurrentBattleOrder.Count == 0)
        {
            InitializeOrder();
            return;
        }
        isPlayerTurn = PlayerBattleList.Contains(CurrentBattleOrder[0]);
        if (isPlayerTurn)
            UIBattleMap.instance.EndTurnButton.SetActive(true);
        else
            CurrentBattleOrder[0].gameObject.GetComponent<EnemyBattleAI>().StartTurn();
    }
}
