using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGenerator : MonoBehaviour
{
    [SerializeField]
    private List<Item> PossibleLoot;
    [SerializeField]
    private List<int> DropChance;
    [SerializeField]
    private int minGold = 0;
    [SerializeField]
    private int maxGold;
    //[SerializeField]
    //private int maxAmountLoot = 1000;

    public int Gold;
    public List<Item> Items = new List<Item>();

    public void GenerateLoot()
    {
        Gold = Random.Range(minGold, maxGold);

        for (int i = 0; i < PossibleLoot.Count; i++)
        {
            //if (Items.Count < maxAmountLoot)
            if (Random.Range(0, 100) < DropChance[i])
            {
                Items.Add(PossibleLoot[i]);
            }
        }
    }
}
