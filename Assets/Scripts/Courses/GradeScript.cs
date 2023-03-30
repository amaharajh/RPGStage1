using System;
using UnityEngine;
using TMPro;
//using System.Collections.Generic;


public class GradeScript : MonoBehaviour
{
    public CalendarData clockinfo;
    public TextMeshProUGUI[] courseGrades;
    [SerializeField] private BoolValue[] BossKills;
    [SerializeField] private ExamBoolValue[] ExamCompletion;
    [SerializeField] private CourseEvent[] courseinfo;
    [SerializeField] private Inventory playerinventory;
    [SerializeField] private Grades[] courseGradesSO;
   // private Dictionary<int, string> courseGradesDict = new Dictionary<int, string>();


    private void Update()
    {
        CalculateCourseGrades();
    }

    private void CalculateCourseGrades()
    {
        for (int i = 0; i < courseinfo.Length; i++)
        {
            int totalAssignments = 0;
            int completedAssignments = 0;
            int courseCode = courseinfo[i].CourseCode;
            bool isFinal = courseinfo[i].Finals;

            for (int j = 0; j < ExamCompletion.Length; j++)
            {
                if (ExamCompletion[j].courseCode == courseCode)
                {
                    totalAssignments++;

                    if (ExamCompletion[j].RuntimeValue == true)
                    {
                        completedAssignments++;
                    }
                }
            }

            string course_grade = CalculateGrade(totalAssignments, completedAssignments, courseinfo[i]);

            if (isFinal && ShouldUpdateGrade(courseinfo[i]))
            {
                UpdateCourseGrade(courseinfo[i].CourseCode, course_grade);
            }
        }
    }

    private string CalculateGrade(int totalAssignments, int completedAssignments, CourseEvent course)
{

    string grade = "";
    int courseCode = course.CourseCode;

    if (completedAssignments == totalAssignments)
    {
        bool allBossesKilled = true;
        for (int i = 0; i < BossKills.Length; i++)
        {
            if (ExamCompletion[i].courseCode == courseCode && !BossKills[i].RuntimeValue)
            {
                allBossesKilled = false;
                break;
            }
        }

        if (allBossesKilled && playerinventory.CompSysCoins > 35)
        {
            grade = "A";
        }
        else if(playerinventory.CompSysCoins>25)
        {
            grade = "B";
        }
        else if (playerinventory.CompSysCoins < 15 )
        {
            grade = "C"; 
        }
    }
    else if ((completedAssignments >= (int)(totalAssignments / 2)) && (playerinventory.CompSysCoins > 25))
        {
        grade = "B";
        }
    else if ((completedAssignments <= (int)(totalAssignments/2)) && (playerinventory.CompSysCoins <= 25))
        {
        grade = "C";
    }
    else 
    {
        grade = "Incomplete";
    }

    return grade;
}


    private bool ShouldUpdateGrade(CourseEvent course)
    {
        int daysPassed = DeadlineDateCheck(course.year, course.month, course.date);
        return daysPassed >= 7;
    }

  private void UpdateCourseGrade(int courseCode, string grade)
{
    for (int i = 0; i < courseinfo.Length; i++)
    {
        if (courseinfo[i].CourseCode == courseCode)
        {
            courseGradesSO[i].RuntimeValue = grade;
           courseGrades[i].text = "Course: " + courseinfo[i].CourseType + courseinfo[i].CourseCode + "\nGrade :" + grade;
            break;
        }
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
