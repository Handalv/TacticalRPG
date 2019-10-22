using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionRelations : MonoBehaviour
{
    public List<Faction> factions;

    public static FactionRelations instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log("More than 1 instance " + this.GetType().ToString());
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

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

    public void GenerateRelationships()
    {
        //default factions for every game
        SetNewFaction("Bandits", FactionType.Bandits, -70);
        SetNewFaction("Deserters", FactionType.Deserters, -40);
        SetNewFaction("Neutral", FactionType.Neutral, 0);

        SetNewFaction("Player", FactionType.Neutral, 0);
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
