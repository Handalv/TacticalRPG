using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGlobalMap : MonoBehaviour
{
    [SerializeField]
    private GameObject battleMessage;
    [SerializeField]
    private GameObject pauseVision;
    //[SerializeField]
    public GameObject unitsList;
    [SerializeField]
    private GameObject reserve;

    // Setup all tabs visible are false by default
    void Start()
    {
        pauseVision.SetActive(GlobalMap.instance.GAMEPAUSED);
        battleMessage.SetActive(false);
        unitsList.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            OpenUnitsList();
        }
    }

    public void ActiveBattleMessage(bool state)
    {
        battleMessage.SetActive(state);
    }

    public void OpenUnitsList()
    {
        unitsList.SetActive(!unitsList.activeSelf);
    }

    public void SetPauseVision(bool pause)
    {
        pauseVision.SetActive(pause);
    }

    public void InitializeUnits(List<PlayerUnitStats> units)
    {
        foreach(PlayerUnitStats unit in units)
        {
            GameObject go = GameObject.Instantiate(Resources.Load("UnitUI")) as GameObject;
            go.GetComponent<Image>().sprite = unit.icon;
            go.GetComponent<Dragable>().unit = unit;
            go.GetComponent<Dragable>().parentToReturn = reserve.transform;
            go.transform.SetParent(reserve.transform);

        }
    }

    public void StartBattle()
    {
        SceneManager.LoadScene(2);
    }
}
