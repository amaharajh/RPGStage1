using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DungeonBossDoor : Interactable
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



    [SerializeField] private CalendarData currTime; 
    [SerializeField] private CourseEvent deadline; 

    // Start is called before the first frame update
    void Start()
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
         int warningDatePassed = WarningDateCheck(currTime.yy, currTime.actualMonth +1, currTime.date);
        int deadlineDatePassed = DeadlineDateCheck(deadline.year, deadline.month +1, deadline.date);
        bool datecheker = (warningDatePassed > (deadlineDatePassed-7) && warningDatePassed < deadlineDatePassed); 
        if(Input.GetKeyDown(KeyCode.Space)&& playerInRange && datecheker)
        {
                if(open == false && playerInventory.GoldKeys > 0)
                {
                    playerInventory.GoldKeys--; 
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

    public override void OnTriggerEnter2D (Collider2D other)
    {
        int warningDatePassed = WarningDateCheck(currTime.yy, currTime.actualMonth +1, currTime.date);
        int deadlineDatePassed = DeadlineDateCheck(deadline.year, deadline.month +1, deadline.date);
        if((other.CompareTag("Player_Passive")|| other.CompareTag("Player")) && !other.isTrigger && open == false)
        {
            context.Raise();
            playerInRange = true;  
            dialogBox.SetActive(true);
           // dialogText.text = dialog; 
            if(warningDatePassed > (deadlineDatePassed-7) && warningDatePassed < deadlineDatePassed)
            {
                dialogText.text = "It is time..Come Forth!"; 
            }
            else
            {
                dialogText.text = "Your time has not arrived...Leave!";
            }
                
            
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
     private int WarningDateCheck(int year, int month, int date)
    {
        int warningDaysPassed = DaysPassed(year, month, date);
        return warningDaysPassed; 
    }
    private int DeadlineDateCheck(int year, int month, int date)
    {
        int deadlineDaysPassed = DaysPassed(year, month, date);
        return deadlineDaysPassed; 
    }

     private static int DaysPassed(int year, int month, int day)
    {
        DateTime startDate = new DateTime(2022, 1, 1);
        DateTime endDate = new DateTime(year, month, day);
        TimeSpan daysPassed = endDate - startDate;
        return daysPassed.Days;
    }
    
}
