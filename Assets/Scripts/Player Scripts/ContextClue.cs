using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{
    public GameObject contextClue; 
    public bool contextActive = false; 
    // Start is called before the first frame update
   public void ChangeContext()
   {
    contextActive = !contextActive; 
    if(contextActive)
    {
        contextClue.SetActive(true);
    }else{
        contextClue.SetActive(false);
    }
   }
}
