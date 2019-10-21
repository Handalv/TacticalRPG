using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGlobalMap : MonoBehaviour
{
    [SerializeField]
    private GameObject battleMessagePanel;
    [SerializeField]
    private GameObject pauseVisionPanel;
    //[SerializeField]
    public GameObject unitsListPanel;
    [SerializeField]
    private GameObject reservePanel;
    [SerializeField]
    private UICity cityUI;

    public TextMeshProUGUI playerGoldText;

    public static UIGlobalMap instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Setup all tabs visible are false by default
    void Start()
    {
        pauseVisionPanel.SetActive(GlobalMap.instance.GAMEPAUSED);
        battleMessagePanel.SetActive(false);
        unitsListPanel.SetActive(false);
        cityUI.cityPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            //OpenUnitsList();
            unitsListPanel.SetActive(!unitsListPanel.activeSelf);
        }
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    //OpenUnitsList();
        //    cityUI.SetActive(!cityUI.activeSelf);
        //}
    }

    public void OpenCityUI(City city)
    {
        cityUI.SetCityInfo(city);
        cityUI.cityPanel.SetActive(true);
    }

    public void ActiveBattleMessage(bool state)
    {
        battleMessagePanel.SetActive(state);
    }

    //public void OpenUnitsList()
    //{
    //    unitsListPanel.SetActive(!unitsListPanel.activeSelf);
    //}

    public void SetPauseVision(bool pause)
    {
        pauseVisionPanel.SetActive(pause);
    }

    public void InitializeUnits(List<PlayerUnitStats> units)
    {
        foreach(PlayerUnitStats unit in units)
        {
            GameObject go = GameObject.Instantiate(Resources.Load("UnitUI")) as GameObject;
            go.GetComponent<Image>().sprite = unit.icon;
            go.GetComponent<Dragable>().unit = unit;
            go.GetComponent<Dragable>().parentToReturn = reservePanel.transform;
            go.transform.SetParent(reservePanel.transform);

        }
    }

    public void AddUnitOnUI(PlayerUnitStats unit)
    {
        GameObject go = GameObject.Instantiate(Resources.Load("UnitUI")) as GameObject;
        //if (unit.icon != null)
            go.GetComponent<Image>().sprite = unit.icon;
        go.GetComponent<Dragable>().unit = unit;
        go.GetComponent<Dragable>().parentToReturn = reservePanel.transform;
        go.transform.SetParent(reservePanel.transform);
    }

    public void StartBattle()
    {
        SceneManager.LoadScene(2);
    }
}
