using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class CoinTextManager : MonoBehaviour
{
    public Inventory playerInventory; 
    public TextMeshProUGUI CoinDisplay; 
    public void UpdateCoinCount()
    {
        CoinDisplay.text = ""+playerInventory.CompSysCoins;
      //  CoinDisplay.text = playerInventory.CompSysCoins.ToString("0000");
    }
}
