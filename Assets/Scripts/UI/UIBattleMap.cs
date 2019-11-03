using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIBattleMap : MonoBehaviour
{
    public GameObject EndTurnButton;
    public GameObject BattleOrderPanel;

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
        EndTurnButton.SetActive(false);
        Destroy(BattleOrderPanel.transform.GetChild(0).gameObject);
        BattleController.instance.RemoveFromOrder();
    }

    public void EndBattle()
    {
        SaveManager.instance.BattleResultSave();
        SceneManager.LoadScene(1);
    }
}
