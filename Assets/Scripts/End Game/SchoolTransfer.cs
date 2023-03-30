using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SchoolTransfer : MonoBehaviour
{
    [SerializeField] private GameObject fadeInPanel;
    [SerializeField] private GameObject fadeoutPanel; 
    [SerializeField] private CalendarData currTime; 
    [SerializeField] private float fade_wait; 
    [SerializeField] private CourseEvent[] listOfEvents;  
    private string exitspawnName;

    public void OnTriggerEnter2D()
    {
        choosingEvent();
    }

    public void choosingEvent()
    {
        int date = currTime.date; 
        int hour = currTime.hh; 
        int month = currTime.actualMonth; 
        int year = currTime.yy; 
        PlayerMovement.spawnPointName = exitspawnName;

        CourseEvent currentEvent = null;

        // Find the current event
        foreach (CourseEvent courseEvent in listOfEvents)
        {
            if (date == courseEvent.date && month == courseEvent.month && year == courseEvent.year)
            {
                currentEvent = courseEvent;
                break;
            }
        }

        // Determine the target scene and exit spawn name based on the current event
        if (currentEvent != null)
        {
            switch (currentEvent.EventType)
            {
                case 1:
                    exitspawnName = "FinalDoor";
                    StartCoroutine(FadeCo("ExamRoom"));
                    break;
                case 3:
                    exitspawnName = "OrientationDoor";
                    StartCoroutine(FadeCo("Orientation"));
                    break;
                case 4:
                    exitspawnName = "ProgressDoor";
                    StartCoroutine(FadeCo("Class Presentation"));
                    break;
                default:
                    exitspawnName = "ClassDoor";
                    StartCoroutine(FadeCo("Class"));
                    break;
            }
        }
        else
        {
            exitspawnName = "ClassDoor";
            StartCoroutine(FadeCo("Class"));
        }
    }

    private IEnumerator FadeCo(string sceneName)
    {
        if (fadeoutPanel != null)
        {
            Instantiate(fadeoutPanel, Vector3.zero, Quaternion.identity); 
        }
        yield return new WaitForSeconds(fade_wait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncOperation.isDone)
        {
            yield return null; 
        }
    }
}
