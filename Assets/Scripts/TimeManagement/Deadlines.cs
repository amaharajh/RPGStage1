using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
public class Deadlines : CourseDeadlines
{

    public override void WarningCheck()
    {
        for (int i = 0; i < courseinfo.Length; i++)
        {
            int warningDatePassed = WarningDateCheck(clockinfo.calendarData.yy, clockinfo.calendarData.actualMonth +1, clockinfo.calendarData.date);
            int deadlineDatePassed = DeadlineDateCheck(courseinfo[i].year, courseinfo[i].month +1, courseinfo[i].date);
        
             
           
            courseDeadlines[i].text = courseinfo[i].CourseType + courseinfo[i].CourseCode.ToString() + "\n" + courseinfo[i].date + "," + clockinfo.scriptableMonths[courseinfo[i].month].monthName.ToString() + "," + courseinfo[i].year;
            
        }
    }

}
