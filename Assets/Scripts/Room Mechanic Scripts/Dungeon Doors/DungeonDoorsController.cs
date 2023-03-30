using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoorsController : MonoBehaviour
{
    public GameObject DungeonDoor; 
    public bool DungeonDoorOpened = false; 
    // Start is called before the first frame update
   public void ChangeDoorState()
   {
    DungeonDoorOpened = !DungeonDoorOpened; 
    if(DungeonDoorOpened)
    {
        DungeonDoor.SetActive(false);
    }else{
        DungeonDoor.SetActive(true);
    }
   }
}
