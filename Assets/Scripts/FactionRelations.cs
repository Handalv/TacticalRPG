using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionRelations : MonoBehaviour
{
    public List<Faction> factions;

    public Faction SetNewFaction(string title, FactionType type, int baseRelation)
    {
        Faction newFaction = new Faction();
        newFaction.title = title;
        newFaction.type = type;
        newFaction.baseRelation = baseRelation;
        newFaction.relations = new List<int>();

        if (factions.Count > 0)
            foreach (Faction faction in factions)
            {
                faction.relations.Add(newFaction.baseRelation);
                newFaction.relations.Add(faction.baseRelation);
            }
        factions.Add(newFaction);
        return newFaction;
    }
}

[Serializable]
public class Faction //can be struct
{
    public int baseRelation;
    public string title;
    public FactionType type;
    public List<int> relations;
}

[Serializable]
public enum FactionType
{
    Neutral,
    Player,
    Bandits,
    Deserters,
    Kingdom,
    FreeCity
}
