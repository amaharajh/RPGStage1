using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeClock : MonoBehaviour
{
	//public SOMont[] scriptableMonths =  new SOMont[12];
	public SOMont[] scriptableMonths;

	public SOMont month;

	//TimeClock Properties
	public int yy;
	public int date;
	public int actualMonth;
	public int hh;
	public int mm;
	private Coroutine repeatcoroutine; 


	public float secondSpeed;

		


	int hoursPassed;
   
	// Start is called before the first frame update
	void Start()
	{
		//month = scriptableMonths[8];
		//InvokeRepeating("TimePasses", secondSpeed, secondSpeed);
		yy = 2022; 
		actualMonth = month.monthPos - 1; 
		//month = scriptableMonths[actualMonth];
	repeatcoroutine = StartCoroutine(RepeatMethod(secondSpeed));
	}
	// Update is called once per frame
	
	void FixedUpdate()
	{
		if(actualMonth <= 11)
		{
			month = scriptableMonths[actualMonth];
		}
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
            yield return new WaitForSeconds(secondSpeed);
            TimePasses();
        }
    }
	int GetDays(DayOfWeek day)
    {
        switch (day)
        {
            case DayOfWeek.Monday: return 1;
            case DayOfWeek.Tuesday: return 2;
            case DayOfWeek.Wednesday: return 3;
            case DayOfWeek.Thursday: return 4;
            case DayOfWeek.Friday: return 5;
            case DayOfWeek.Saturday: return 6;
            case DayOfWeek.Sunday: return 0;
        }

        return 0;
    }
	public void TimePasses() //Sets the IngameTime passing
	{
		mm++;
		if (mm > 59)
		{
			mm = 0;
			hh++;
			hoursPassed++;
			CheckClock();
			
		}
	}
	private void CheckClock() 
	{
		if (hh > 23)
		{
			hh = 0;
			date++;
		//	Debug.Log("date has incremented");
			//Debug.Log("" + actualMonth);
			if (date > month.dayAmmount)
			{
				if(actualMonth <11)
				{
					date = 1;
					actualMonth++;	
					//Debug.Log("month has incremented, month is now: " + actualMonth);			
					month = scriptableMonths[actualMonth];	
				}			
				else 
				{
					date = 1;
					actualMonth = 0;
					month = scriptableMonths[actualMonth];	
					yy++;
					//Debug.Log("year has incremented"); 
				}
				
			}
		}
	}

}

