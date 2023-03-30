using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LectureDoor : Interactable
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

    //[SerializeField] private BoolValue examComplted; 
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

            for(int x = 0; x <courseinfo[i].daysOfWeek.Length; x++)
            {
                int currDay = currtime.dayOfWeek;
                int lectureDay = courseinfo[i].daysOfWeek[x]; 

                if(Input.GetKeyDown(KeyCode.Space)&& playerInRange )
                {

                        if((currDay == lectureDay) && (currtime.hh == courseinfo[i].hours))
                        {
                        DoorHasOpened.Raise(); 
                        open = true; 
                        
                        }
                    
                
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
            for(int x = 0; x <courseinfo[i].daysOfWeek.Length; x++)
            {

            
                int currDay = currtime.dayOfWeek;
                int lectureDay = courseinfo[i].daysOfWeek[x];  
                if(other.CompareTag("Player_Passive") && !other.isTrigger)
                {
                    context.Raise();
                    playerInRange = true;  
                    dialogBox.SetActive(true);

                    if((currDay == lectureDay) && (currtime.hh == courseinfo[i].hours))
                    {                       
                        dialogText.text = "Its Time to go..."; 
                        break;
                    }
                    if((currDay == lectureDay) && (currtime.hh < courseinfo[i].hours))
                        {                    
                        dialogText.text = "It isn't time for class yet...i'll wait it out....."; 
                        break;
                        }
                    if((currDay == lectureDay) && (currtime.hh > courseinfo[i].hours))
                        {
                        dialogText.text = "Oh crap..i missed my class...."; 
                        break;
                        }
                    if(currDay != lectureDay)
                    {
                        dialogText.text = "I don't have class right now...why am i here again?.."; 
                        break;
                    }
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
