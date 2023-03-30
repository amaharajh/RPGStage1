using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeClockTest : MonoBehaviour
{
    public SOMont[] scriptableMonths;
    public SOMont month;
    public CalendarData calendarData; 

    // TimeClock Properties
  
    private int[] daysInMonth = new int[] { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334 };
    
    int hoursPassed;
    private Coroutine repeatcoroutine;

    public float secondSpeed;

   public string[] daysOfWeek = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
  
    private int startingDay; // starting day of the year (0 = Sunday, 1 = Monday, etc.)

    void Start()
    {
         
        // Set starting day of the year (January 1st)
        DateTime startingDate = new DateTime(2022, 1, 1);
        startingDay = (int)startingDate.DayOfWeek;
       
       

       // calendarData.yy = 2022;
       // calendarData.actualMonth = month.monthPos - 1;
        // monthReference = actualMonth; 
         month = scriptableMonths[calendarData.actualMonth];
        calendarData.daysPassed = DaysPassed(calendarData.yy, calendarData.actualMonth +1, calendarData.date); 
        calendarData.dayOfWeek = ((calendarData.daysPassed) + (startingDay)) % 7;
        repeatcoroutine = StartCoroutine(RepeatMethod(secondSpeed));
    }

    private void StopRepeating()
    {
        if (repeatcoroutine != null)
        {
            StopCoroutine(repeatcoroutine);
            repeatcoroutine = null;
        }
    }

    private IEnumerator RepeatMethod(float secondSpeed)
    {
        while (true)
        {
          //  Debug.Log("days passed: " + daysPassed);
            yield return new WaitForSeconds(secondSpeed);
            TimePasses();
        }
    }

    public void TimePasses() //Sets the IngameTime passing
    {
       calendarData.mm++;
       calendarData.maxMonthDays = month.dayAmmount; 
        if (calendarData.mm > 59)
        {
            calendarData.mm = 0;
            calendarData.hh++;
            hoursPassed++;
            CheckClock();
        }
    }

    private void CheckClock()
    {
        if (calendarData.hh > 23)
        {
            calendarData.hh = 0;
            calendarData.date++;
            
          

            if (calendarData.date > month.dayAmmount)
            {
                //monthReference++;
                if (calendarData.actualMonth < 11)
                {
                    calendarData.date = 1;
                    calendarData.actualMonth++;
                 //   month = scriptableMonths[calendarData.actualMonth];
                    
                }
                else
                {
                    calendarData.date = 1;
                    calendarData.actualMonth = 0;
                   // month = scriptableMonths[calendarData.actualMonth];
                    calendarData.yy++;
                }
                 month = scriptableMonths[calendarData.actualMonth];
            }
        }


        calendarData.daysPassed = DaysPassed(calendarData.yy, calendarData.actualMonth +1, calendarData.date); 
      //  Debug.Log("days passed: " + daysPassed);
        calendarData.dayOfWeek = ((calendarData.daysPassed) + (startingDay)) % 7;
       // Debug.Log("Today is " + daysOfWeek[dayOfWeek]);
    }

private int GetWeekNumber(int day)
{
    // Calculate the week number by dividing the number of days passed by 7 and rounding up to the nearest integer
    int week = (int)Math.Ceiling((double)(day + startingDay) / 7);
    
    // If the week is greater than 4, reset it to 1 and increment the actual month
    if (week > 4)
    {
        week = 1;
        calendarData.actualMonth++;

        // If the actual month is greater than 11 (December), reset it to 0 (January) and increment the year
        if (calendarData.actualMonth > 11)
        {
            calendarData.actualMonth = 0;
            calendarData.yy++;
        }

        // Set the month variable to the new actual month
        month = scriptableMonths[calendarData.actualMonth];
    }

    return week;
}

    public static int DaysPassed(int year, int month, int day)
    {
        DateTime startDate = new DateTime(2022, 1, 1);
        DateTime endDate = new DateTime(year, month, day);
        TimeSpan daysPassed = endDate - startDate;
        return daysPassed.Days;
    }
}
