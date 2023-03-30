using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Inventory : ScriptableObject
{
    public Item currentItem ; 
    public List<Item> items = new List<Item>();
    public int CopperKeys; 
    public int SilverKeys; 
    public int GoldKeys;
    public int CompSysCoins;
    public int CommSysCoins; 
    public int ControlSysCoins; 
    public int ElecPowerCoins; 
    public int AccSysCoins; 

    public void AddItem(Item itemToAdd)
    {
        // is the item a key? 
        if (itemToAdd.CopperKey)
        {
            CopperKeys++; 
        }else if(itemToAdd.SilverKey)
        {
            SilverKeys++; 
        }
        else if (itemToAdd.GoldKey)
        {
            GoldKeys++;
        }
        else
        {
            if(!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }


}
