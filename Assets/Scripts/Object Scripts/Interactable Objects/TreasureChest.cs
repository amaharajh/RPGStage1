using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    public Item contents; 
    public Inventory playerInventory; 
    public bool isOpen; 
    public BoolValue storedOpen; 
    public SignalSender raiseItem;
    public GameObject dialogBox; 
    public Text dialogText; 
    private Animator anim; 

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); 
        isOpen = storedOpen.RuntimeValue; 
        if (isOpen)
        {
            anim.SetBool("opened", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
           if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
            {
                if (!isOpen)
                {
                    //Open the chest
                     OpenChest();
                }else
                {
                    //chest is already open 
                     ChestAlreadyOpen();
                }
             }
        
    }

    public void OpenChest()
    {
       //Dialog Window on
       dialogBox.SetActive(true);
       //Dialog Text = contents Text
       dialogText.text = contents.itemDescription;
       //add contents to the inventory 
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents; 

       //Raise signal to player to animate
       raiseItem.Raise();
       
       //Raise the context clue
       context.Raise();
       //Set chest to opened
       isOpen = true; 
       anim.SetBool("opened", true);
       storedOpen.RuntimeValue = isOpen; 
    }

    public void ChestAlreadyOpen()
    {
        playerInRange = false; 
       //Dialog Window off
       dialogBox.SetActive(false);
    
       //Raise signal to player to stop animating
       raiseItem.Raise();
      }


public override void OnTriggerEnter2D (Collider2D other)
    {

        if(other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = true; 
        }
    }

public override void OnTriggerExit2D(Collider2D  other)
    {

        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = false; 
        
        }
    }
}