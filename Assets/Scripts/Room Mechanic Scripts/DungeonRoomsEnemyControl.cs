using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoomsEnemyControl : DungeonRooms
{
    public float waittime; 
    public DoorObject[] doors; 

    /*public override void Start()
    {
        for(int i = 0; i < doors.Length; i++)
            {
            ChangeActivation(doors[i], false); 
            }
      
    }*/
public int EnemiesActive()
    {
        int activeEnemies = 0;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.activeInHierarchy)
            {
                activeEnemies++;
            }
        }
        return activeEnemies;
    }

 public void CheckEnemies()
    {
        if (EnemiesActive() == 1)
        {
            Debug.Log("doors will open");
             for(int i = 0; i < doors.Length; i++)
            {
                ChangeActivation(doors[i], false); 
            }
        }
    } 
    
   
    public override void OnTriggerEnter2D(Collider2D other)
  {
    
    if(other.CompareTag("Player") && !other.isTrigger)
    {
        
        for (int i = 0; i < enemies.Length; i++)
        {
            ChangeActivation(enemies[i], true); 
        }
         for (int i = 0; i < pots.Length; i++)
        {
            ChangeActivation(pots[i], true); 
        }
        virtualCamera.SetActive(true); 
        if(enemies.Length > 0)
        StartCoroutine(DoorCloseWaitCo());
      
    }
  }
    
  public override void OnTriggerExit2D(Collider2D other)
  {
     if(other.CompareTag("Player") && !other.isTrigger)
    {
         for (int i = 0; i < enemies.Length; i++)
        {
            ChangeActivation(enemies[i], false); 
        }
         for (int i = 0; i < pots.Length; i++)
        {
            ChangeActivation(pots[i], false); 
        }
        virtualCamera.SetActive(false); 
    }
  } 

    private  IEnumerator DoorCloseWaitCo()
    {
        
        yield return new WaitForSecondsRealtime(waittime);
          for(int i = 0; i < doors.Length; i++)
        {
            ChangeActivation(doors[i], true); 
        }
}
}
