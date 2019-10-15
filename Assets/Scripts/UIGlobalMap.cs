using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGlobalMap : MonoBehaviour
{
    [SerializeField]
    private GameObject battleMessage;
    [SerializeField]
    private GameObject pauseVision;

    // Setup all tabs visible are false by default
    void Start()
    {
        if (battleMessage == null)
            Debug.Log("Battle message is null on UI");
        pauseVision.SetActive(GlobalMap.instance.GAMEPAUSED);
        battleMessage.SetActive(false);
    }

    public void OpenBattleMessage()
    {
        battleMessage.SetActive(true);
    }

    public void SetPauseVision(bool pause)
    {
        pauseVision.SetActive(pause);
    }

    public void StartBattle()
    {
        SceneManager.LoadScene(2);
    }
}
