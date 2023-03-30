using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum DoorType2
{
    key, 
    enemy, 
    doorbutton,
    hubReturn
}

public class DungeonDoors : Interactable
{
    


    public SignalSender DoorHasOpened;
    public Inventory playerInventory;
    public Text dialogText; 
     public GameObject dialogBox; 
    public string dialog; 
    private SpriteRenderer doorSprite;
    private BoxCollider2D physicsCollider;
    public DoorType2 thisDoorType;
    public bool open = false;  
    public BoolValue DoorIsOpen; 

    private void Start() 
    {

        doorSprite = GetComponent<SpriteRenderer>();
        physicsCollider = GetComponent<BoxCollider2D>();

        open = DoorIsOpen.RuntimeValue;
        if(open)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&& playerInRange && (thisDoorType == DoorType2.key))
        {
                if(open == false && playerInventory.CopperKeys > 0)
                {
                    playerInventory.CopperKeys--; 
                    //&& (playerInventory.numberofkeys > 0)
                //OpenDooDungeonr();
                DoorHasOpened.Raise(); 
                open = true; 
                DoorIsOpen.RuntimeValue = open; 
                }
        } 
    }

public void OpenDooDungeonr() 
{
//Turn off the door's sprite renderer
        
        //set open to true
        open = true;
        doorSprite.enabled = false;
        //turn off the door's box collider
        physicsCollider.enabled = false;
        DoorIsOpen.RuntimeValue = open; 
}


public void DoorIsClose() 
{
 //Turn off the door's sprite renderer
        doorSprite.enabled = true;
        //set open to true
        open = false;
        //turn off the door's box collider
        physicsCollider.enabled = true;
}

public override void OnTriggerEnter2D (Collider2D other)
    {

        if((other.CompareTag("Player_Passive")|| other.CompareTag("Player")) && !other.isTrigger && open == false)
        {
            context.Raise();
            playerInRange = true;  
            dialogBox.SetActive(true);
            dialogText.text = dialog; 
                
            
        }
    }

    public override void OnTriggerExit2D(Collider2D  other)
    {

        if ((other.CompareTag("Player_Passive")|| other.CompareTag("Player")) && !other.isTrigger)
        {
            context.Raise();
            playerInRange = false; 
            dialogBox.SetActive(false);
        
        }
    }

 

}


