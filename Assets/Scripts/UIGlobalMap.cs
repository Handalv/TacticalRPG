using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGlobalMap : MonoBehaviour
{
    [SerializeField]
    private GameObject battleMessage;


    // Setup all tabs visible are false by default
    void Start()
    {
        if (battleMessage == null)
            Debug.Log("Battle message is null on UI");
        battleMessage.SetActive(false);
    }


    public void StartBattle()
    {
        //TODO Open BattleMap
    }
}
