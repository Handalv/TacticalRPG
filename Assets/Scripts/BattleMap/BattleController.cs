using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    public bool isPlayerTurn;

    public List<BattleUnit> BattleOrder = null;
    public List<BattleUnit> CurrentBattleOrder = null;

    public List<BattleUnit> EnemyBattleList = null;
    public List<BattleUnit> PlayerBattleList = null;

    UnitList playerUnits;
    [HideInInspector]
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
        //TEST
        int index = 0;

        int offsetX = (map.mapSizeX / 2) + (map.mapSizeX / 4);
        int x = 0;
        int z = 0;
        foreach (UnitPreset preset in GameSettings.instance.Enemies)
        {
            CreachureStats creachure = new CreachureStats(preset); 
            GameObject spawned = GameObject.Instantiate(Resources.Load("EnemyBattle")) as GameObject;

            spawned.name = "Enemy " + index;
            index++;

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

    public void RemoveFromOrder(int index = 0)
    {
        CurrentBattleOrder.RemoveAt(index);
        GameObject CurrentUnitIcon = UIBattleMap.instance.BattleOrderPanel.transform.GetChild(index).gameObject;
        Destroy(CurrentUnitIcon);
        if (CurrentBattleOrder.Count == 0)
        {
            InitializeOrder();
            return;
        }
    }

    public void EndTurn()
    {
        RemoveFromOrder();

        isPlayerTurn = PlayerBattleList.Contains(CurrentBattleOrder[0]);

        UIBattleMap.instance.EndTurnButton.SetActive(isPlayerTurn);
        if (!isPlayerTurn)
        {
            StartCoroutine(EnemyturnCD());
        }
        else
        {
            BattlePlayerControls.instance.selectedUnit = CurrentBattleOrder[0];
            //TODO Move it into UI script
            foreach (FightAction action in CurrentBattleOrder[0].Actions)
            {
                GameObject go = Instantiate(Resources.Load("SkillButton"),UIBattleMap.instance.UnitSkillPanel.transform) as GameObject;
                UIBattleMap.instance.SkillList.Add(go);

                BattleSkillButton bsb = go.GetComponent<BattleSkillButton>();
                bsb.CostText.text = ""+action.Cost;
                bsb.Icon.sprite = action.icon;
                bsb.action = action;
                bsb.unit = CurrentBattleOrder[0];
                UIBattleMap.instance.SkillList.Add(go);
            }
        }
    }

    IEnumerator EnemyturnCD()
    {
        yield return new WaitForSeconds(0.1f);
        CurrentBattleOrder[0].gameObject.GetComponent<EnemyMeleeAI>().StartTurn();
    }

    public void Defeat()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Defeat");
    }

    public void Victory()
    {
        SaveManager.instance.BattleResultSave();
        SaveManager.instance.save.MapObjectName.RemoveAt(SaveManager.instance.save.BattleOpponentIndex);
        SaveManager.instance.save.MapObjectX.RemoveAt(SaveManager.instance.save.BattleOpponentIndex);
        SaveManager.instance.save.MapObjectZ.RemoveAt(SaveManager.instance.save.BattleOpponentIndex);
        SaveManager.instance.isLoadGame = true;
        SceneManager.LoadScene(1);
        Debug.Log("Victory");
    }
}
