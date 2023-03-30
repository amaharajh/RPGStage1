using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionNumber : MonoBehaviour
{
   public int doornum; 

   void OnTriggerEnter2D(Collider2D other)
   {
    if(other.CompareTag("Player") && !other.isTrigger)
    {
  
    }
   } 
}
