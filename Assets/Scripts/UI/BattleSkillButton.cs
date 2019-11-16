using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSkillButton : MonoBehaviour
{
    public TextMeshProUGUI CostText;
    public Image Icon;

    public BattleUnit unit;
    public FightAction action;
    bool isEnabled = true;

    public void Click()
    {
        if (isEnabled)
        {
            //UNDONE
            //action.Use(unit,unit);
            action.CheckValidTargets(unit);
        }
    }
}
