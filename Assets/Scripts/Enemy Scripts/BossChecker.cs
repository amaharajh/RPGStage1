using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossChecker : MonoBehaviour
{
    [SerializeField] private CalendarData calendarinfo; 
    [SerializeField] private CourseEvent[] courseInfo; 
    [SerializeField] private BoolValue[] bosschecker;
    [SerializeField] private BossScript[] boss;  
    // Start is called before the first frame update
    void Awake()
    {

        WarningCheck();
       
    }

 public virtual void WarningCheck()
    {
        for(int i = 0; i <courseInfo.Length; i++)
        {
            int warningDatePassed = WarningDateCheck(calendarinfo.yy, calendarinfo.actualMonth +1, calendarinfo.date);
            Debug.Log("the warning date for " + courseInfo[i].CourseCode + courseInfo[i].CourseAssignment + " is :" + warningDatePassed);
            int deadlineDatePassed = DeadlineDateCheck(courseInfo[i].year, courseInfo[i].month +1, courseInfo[i].date);
            Debug.Log("the deadline date for " + courseInfo[i].CourseCode + courseInfo[i].CourseAssignment + " is :" + deadlineDatePassed);
            if(warningDatePassed >= (deadlineDatePassed - 3) && warningDatePassed < deadlineDatePassed)
            {
               if((boss[i].courseAssignmentCode == courseInfo[i].CourseAssignmentCode) && bosschecker[i].RuntimeValue == false)
               {
                boss[i].gameObject.SetActive(true);
               }
                /*
                if(bosschecker[i].RuntimeValue == false)
                {
                    boss[i].SetActive(true);
                }
                else
                {
                    boss[i].SetActive(false);
                } */
            }     
        }
    }

 protected int WarningDateCheck(int year, int month, int date)
    {
        int warningDaysPassed = DaysPassed(year, month, date);
        return warningDaysPassed; 
    }
    protected int DeadlineDateCheck(int year, int month, int date)
    {
        int deadlineDaysPassed = DaysPassed(year, month, date);
        return deadlineDaysPassed; 
    }
    protected static int DaysPassed(int year, int month, int day)
    {
        DateTime startDate = new DateTime(2022, 1, 1);
        DateTime endDate = new DateTime(year, month, day);
        TimeSpan daysPassed = endDate - startDate;
        return daysPassed.Days;
    }
    
}
