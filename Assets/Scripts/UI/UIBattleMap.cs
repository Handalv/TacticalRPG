using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIBattleMap : MonoBehaviour
{
    public GameObject EndTurnButton;
    public GameObject BattleOrderPanel;
    public GameObject UnitSkillPanel;
    public List<GameObject> SkillList = null;

    public TextMeshProUGUI ActionPointsText;
    public GameObject MapObjectElementsPanel;

    public static UIBattleMap instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log("More than 1 instance " + this.GetType().ToString());
            Destroy(this);
        }

        if(MapObjectElementsPanel == null)
        {
            MapObjectElementsPanel = new GameObject();
            MapObjectElementsPanel.transform.SetParent(this.transform);
        }
    }

    public void EndTurn()
    {
        SkillList.Clear();
        for (int i = UnitSkillPanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(UnitSkillPanel.transform.GetChild(i).gameObject);
        }
        BattlePlayerControls.instance.CurrentPath = null;
        BattlePlayerControls.instance.selectedUnit = null;
        BattleController.instance.EndTurn();
    }

    //TEST
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            BattleController.instance.Victory();
        }
    }
}
