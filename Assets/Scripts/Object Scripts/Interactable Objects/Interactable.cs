using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public SignalSender context;
     public bool playerInRange;  

    public virtual void OnTriggerEnter2D (Collider2D other)
    {

        if((other.CompareTag("Player") ||other.CompareTag("Player_Passive")) && !other.isTrigger)
        {
            context.Raise();
            playerInRange = true; 
        }
    }

    public virtual void OnTriggerExit2D(Collider2D  other)
    {

         if((other.CompareTag("Player") ||other.CompareTag("Player_Passive")) && !other.isTrigger)
        {
            context.Raise();
            playerInRange = false; 
        
        }
    }
}
