using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRooms : MonoBehaviour
{
  public Enemy [] enemies; 
  public pot[] pots; 
  public GameObject virtualCamera; 

  public virtual void OnTriggerEnter2D(Collider2D other)
  {
    if((other.CompareTag("Player") || other.CompareTag("Player_Passive")) && !other.isTrigger)
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
    }
  }

  public virtual void OnTriggerExit2D(Collider2D other)
  {
     if((other.CompareTag("Player") || other.CompareTag("Player_Passive")) && !other.isTrigger)
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

  public void OnDisable()
  {
    virtualCamera.SetActive(false); 
  }
 public void ChangeActivation(Component component, bool activation)
  {
    component.gameObject.SetActive(activation);
  }
}
