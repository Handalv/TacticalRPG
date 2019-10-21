using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIBattleMap : MonoBehaviour
{
    public static UIBattleMap instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void EndBattle()
    {
        SceneManager.LoadScene(1);
    }
}
