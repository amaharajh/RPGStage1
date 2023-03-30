using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CalendarData", menuName = "ScriptableObjects/CalendarData")]
[System.Serializable]
public class CalendarData : ScriptableObject
{
    public int mm;
    public int hh;
    public int date;
    public int actualMonth;
    public int yy;
    public int daysPassed;
    public int dayOfWeek;
    public int maxMonthDays; 
    
    [HideInInspector]
    public int initial_mm = 0;

    [HideInInspector]
    public int initial_hh = 8;
    
    [HideInInspector]
    public int initial_date = 4;

    [HideInInspector]
    public int initial_actualMonth = 8;

    [HideInInspector]
    public int initial_yy = 2022;
    
    // Add any other necessary calendar properties here
}

