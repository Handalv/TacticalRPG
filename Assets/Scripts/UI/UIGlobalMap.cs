using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIGlobalMap : MonoBehaviour
{
    [SerializeField]
    private GameObject battleMessagePanel;
    [SerializeField]
    private GameObject pauseVisionPanel;
    [SerializeField]
    private GameObject reservePanel;
    [SerializeField]
    private GameObject battleFieldPanel;
    [SerializeField]
    private GameObject esqPlanel;
    [SerializeField]
    private UICity cityUI;

    //[SerializeField]
    public GameObject unitsListPanel;
    public GameObject playerGoldText;
    public GameObject MapObjectElementsPanel;

    public static UIGlobalMap instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Setup visibility of all ui panels is false by default
    void Start()
    {
        pauseVisionPanel.SetActive(GlobalMap.instance.GAMEPAUSED);
        battleMessagePanel.SetActive(false);
        unitsListPanel.SetActive(false);
        cityUI.CityPanel.SetActive(false);
        esqPlanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            //OpenUnitsList();
            if (battleMessagePanel.activeSelf == false && esqPlanel.activeSelf == false)
            {
                unitsListPanel.SetActive(!unitsListPanel.activeSelf);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            unitsListPanel.SetActive(false);
            cityUI.CityPanel.SetActive(false);
            if (battleMessagePanel.activeSelf == false)
            {
                esqPlanel.SetActive(!esqPlanel.activeSelf);
                GlobalMap.instance.GAMEPAUSED = esqPlanel.activeSelf;
            }
        }
    }

    public void OpenCityUI(City city)
    {
        cityUI.SetCityInfo(city);
        cityUI.CityPanel.SetActive(true);
    }

    public void ActiveBattleMessage(bool state)
    {
        battleMessagePanel.SetActive(state);
    }

    public void SetPauseVision(bool pause)
    {
        pauseVisionPanel.SetActive(pause);
    }

    public void InitializeUnits()
    {
        UnitList unitList = UnitList.instance;
        for(int i=0; i<unitList.units.Count; i++)
        {
            AddUnitOnUI(i, unitList.units[i].icon, unitList.isOnBattleField[i], unitList.BattleFieldIndex[i]);
        }
    }

    public void AddUnitOnUI(int index, Sprite icon,bool isOnBattle, int battleIndex)
    {
        GameObject go = GameObject.Instantiate(Resources.Load("UnitUI")) as GameObject;
        go.GetComponent<Image>().sprite = icon;
        go.GetComponent<Dragable>().IndexInUnitList = index;
        if (isOnBattle)
        {
            go.GetComponent<Dragable>().parentToReturn = battleFieldPanel.transform.GetChild(battleIndex);
        }
        else
            go.GetComponent<Dragable>().parentToReturn = reservePanel.transform;
        go.transform.SetParent(go.GetComponent<Dragable>().parentToReturn);
    }

    public void StartBattle()
    {
        SaveManager.instance.SaveGame();
        SceneManager.LoadScene(2);
    }
}
