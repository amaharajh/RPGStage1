using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class ExamDoor : Interactable
{
   public SignalSender DoorHasOpened;
    public Text dialogText; 
    public GameObject dialogBox; 
    public GameObject door; 
    private SpriteRenderer doorSprite;
    private BoxCollider2D physicsCollider;
    public bool open = false;  
   
    public CourseEvent[] courseinfo; 
    public CalendarData currtime; 

    // Start is called before the first frame update
    private void Start() 
    {

        doorSprite = GetComponent<SpriteRenderer>();
        physicsCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i =  0; i < courseinfo.Length; i++)
        {

        
            int warningDatePassed = WarningDateCheck(currtime.yy, currtime.actualMonth +1, currtime.date);
            int deadlineDatePassed = DeadlineDateCheck(courseinfo[i].year, courseinfo[i].month +1, courseinfo[i].date);

            if(Input.GetKeyDown(KeyCode.Space)&& playerInRange )
            {

                    if((warningDatePassed == deadlineDatePassed) && (currtime.hh == courseinfo[i].hours))
                    {
                    DoorHasOpened.Raise(); 
                    open = true; 
                    
                    }
                
            
            } 
        }
    }
    public void OpenDooDungeonr() 
    {
    //Turn off the door's sprite renderer
            
        door.SetActive(false);
    }
 
    public override void OnTriggerEnter2D(Collider2D other)
    {
        for(int i =  0; i < courseinfo.Length; i++)
        {
            int warningDatePassed = WarningDateCheck(currtime.yy, currtime.actualMonth +1, currtime.date);
            int deadlineDatePassed = DeadlineDateCheck(courseinfo[i].year, courseinfo[i].month +1, courseinfo[i].date);
            if(other.CompareTag("Player_Passive") && !other.isTrigger)
            {
                context.Raise();
                playerInRange = true;  
                dialogBox.SetActive(true);
              if((warningDatePassed == deadlineDatePassed) &&(currtime.hh == courseinfo[i].hours))
                {
                    dialogText.text = "Its Time to go..."; 
                }
                else if((warningDatePassed != deadlineDatePassed) ||(warningDatePassed == deadlineDatePassed && (currtime.hh != courseinfo[i].hours)))
                    {
                    dialogText.text = "It isn't time yet...i'll wait it out...i'm so nervous........"; 
                    }
            }
        }
    
    }
    public override void OnTriggerExit2D(Collider2D  other)
    {

         if((other.CompareTag("Player") ||other.CompareTag("Player_Passive")) && !other.isTrigger)
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
