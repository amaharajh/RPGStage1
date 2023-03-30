using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Loot
{
    public Resources thisLoot; 
    public int lootChance; 
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots; 

    public Resources LootResources()
    {
        int cummProb = 0; 
        int currentPRob = Random.Range(0, 100); 

        for(int i = 0; i < loots.Length; i++)
        {
            cummProb += loots[i].lootChance; 
            if(currentPRob <= cummProb)
            {
                return loots[i].thisLoot; 
            }
        }
        return null; 
        //accumulative probability 
    }
}

