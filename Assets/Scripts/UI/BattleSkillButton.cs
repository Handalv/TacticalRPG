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

    public void Click()
    {
        if (unit.CurrentActionpoints - action.Cost>=0)
        {
            BattlePlayerControls.instance.isUsingSkill = true;
            BattlePlayerControls.instance.UsingSkill = action;
            action.CheckValidTargets(unit);
            HighlightValidTargets();
        }
    }

    public void HighlightValidTargets()
    {
        Color color = Color.red;
        if (action.isTargetsFriendly)
        {
            color = Color.green;
        }

        color.a = 0.3f;
        foreach (BattleUnit target in action.Validtargets) 
        {
            target.HighlightEnable(color);
        }
    }
}
