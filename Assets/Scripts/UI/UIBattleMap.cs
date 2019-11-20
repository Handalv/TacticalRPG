using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIBattleMap : MonoBehaviour
{
    public GameObject EndTurnButton;
    public GameObject BattleOrderPanel;
    public GameObject UnitSkillPanel;
    public List<GameObject> SkillList = null;

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

    public void EndBattle()
    {
        SaveManager.instance.BattleResultSave();
        SceneManager.LoadScene(1);
    }
}
