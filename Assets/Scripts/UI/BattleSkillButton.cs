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
            HighlightValidTargets();
        }
    }

    public void HighlightValidTargets()
    {
        Debug.Log(""+action.Validtargets.Count);
        Color color = Color.red;
        if (action.isTargetsFriendly)
        {
            color = Color.green;
        }
        foreach (BattleUnit target in action.Validtargets) 
        {
            BattleMap.instance.tiles[target.tileX, target.tileZ].GFX.GetComponent<MeshRenderer>().material.color = color;
            BattleMap.instance.tiles[target.tileX, target.tileZ].warFogEnabled = true;
        }
    }
}
