using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum coinName
{
    CommSysCoin, 
    CompSysCoin, 
    ControlSysCoin, 
    ElecPowerCoin, 
    AccSysCoin
}

public class Coin : Resources
{
   public coinName CoinType; 

    public Inventory playerInventory; 
    // Start is called before the first frame update
    void Start()
    {
          resourceSignal.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            if(CoinType == coinName.CompSysCoin)
            {
                playerInventory.CompSysCoins +=1; 
             }
             if(CoinType == coinName.CommSysCoin)
            {
                playerInventory.CommSysCoins +=1; 
             }
             if(CoinType == coinName.ControlSysCoin)
            {
                playerInventory.ControlSysCoins +=1;               
             }
             if(CoinType == coinName.ElecPowerCoin)
            {
                playerInventory.ElecPowerCoins +=1;  
             }
             if(CoinType == coinName.AccSysCoin)
            {
                playerInventory.AccSysCoins +=1;                
             }


             resourceSignal.Raise(); 
             Destroy(this.gameObject);
         }
    }
}
