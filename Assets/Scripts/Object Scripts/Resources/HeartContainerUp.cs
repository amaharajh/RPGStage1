using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainerUp : Resources
{
    public HeartManager heartinfo; 
    public FloatValue heartContainers; 
    public FloatValue playerHealth; 

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")&& !other.isTrigger)
        {
        
            if(heartContainers.RuntimeValue< (float)heartinfo.hearts.Length)
            {
            heartContainers.RuntimeValue += 1;
            playerHealth.RuntimeValue = heartContainers.RuntimeValue * 2; 
            resourceSignal.Raise(); 
            Destroy(this.gameObject);  
            }
            else{
                Destroy(this.gameObject);
            }
        }
    }
}
