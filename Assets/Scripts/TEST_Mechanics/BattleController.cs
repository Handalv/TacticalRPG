using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public bool isPlayerTurn;

    public List<CreachureStats> BattleOrder = null;
    public List<CreachureStats> CurrentBattleOrder = null;

    public List<CreachureStats> EnemyBattleList = null;
    public List<CreachureStats> PlayerBattleList = null;

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
    }

    void Start()
    {
        UnitList playerUnits = UnitList.instance;
        PlayerBattleList = playerUnits.units;
        foreach (CreachureStats creachure in playerUnits.units)
        {
            BattleOrder.Add(creachure);
        }
        foreach(UnitPreset preset in GameSettings.instance.Enemies)
        {
            CreachureStats creachure = new CreachureStats(preset);
            EnemyBattleList.Add(creachure);
            BattleOrder.Add(creachure);
        }

        // Sorting Battle order by units speed
        InitializeOrder();
    }

    public void InitializeOrder()
    {
        BattleOrder.Sort(delegate (CreachureStats x, CreachureStats y) {
            return x.Speed.CompareTo(y.Speed);
        });
        CurrentBattleOrder = new List<CreachureStats>(BattleOrder);
        foreach (CreachureStats creachure in CurrentBattleOrder)
        {
            GameObject go = new GameObject();
            Image image = go.AddComponent<Image>();
            image.sprite = creachure.icon;
            go.transform.SetParent(UIBattleMap.instance.BattleOrderPanel.transform);
        }
        isPlayerTurn = PlayerBattleList.Contains(CurrentBattleOrder[0]);
    }

    public void RemoveFromOrder()
    {
        CurrentBattleOrder.RemoveAt(0);
        Destroy(UIBattleMap.instance.BattleOrderPanel.transform.GetChild(0).gameObject);
        if (CurrentBattleOrder.Count == 0)
        {
            InitializeOrder();
            return;
        }
        isPlayerTurn = PlayerBattleList.Contains(CurrentBattleOrder[0]);
    }
}
