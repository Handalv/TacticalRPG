using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionRelations : MonoBehaviour
{
    //public FactionType[] factions;
    //public string[] factionNames;
    //public int[,] relations;

    public Faction[] factions;

    void Start()
    {
        foreach (Faction faction in factions) {
            for(int i = 0; i < factions.Length; i++)
            {
                //if (faction.relations.Length < factions.Length)
                //    faction.relations = new int[factions.Length];


                if (factions[i].title == faction.title)
                    continue;


                switch (faction.type)
                {
                    case FactionType.Neutral:
                        faction.relations[i] = 0;
                        break;
                    case FactionType.Bandits:
                        faction.relations[i] = -70;
                        break;
                    case FactionType.Deserters:
                        faction.relations[i] = -40;
                        break;
                    //case FactionType.Player:
                    //    break;
                    //case FactionType.Kingdom:
                    //    break;
                    //case FactionType.FreeCity:
                    //    break;
                    default:
                        faction.relations[i] = 0;
                        break;
                }
            }
        }
                
    }
}

[Serializable]
public struct Faction
{
    public FactionType type;
    public string title;
    public int[] relations;
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
