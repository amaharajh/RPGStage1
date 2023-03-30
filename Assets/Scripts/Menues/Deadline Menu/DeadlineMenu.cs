using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeadlineMenu : MonoBehaviour
{
    protected bool isPaused;
    public GameObject deadlinePanel; 
    public TimeClockTest clock; 
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(Input.GetButtonDown("deadlines"))
        {
            ChangePauseState();
        }
    }

  public void ChangePauseState()
    {
        isPaused = !isPaused; 
        if(isPaused)
            {
                deadlinePanel.SetActive(true);
                Time.timeScale = 0f; 
            }
            else 
            {
                deadlinePanel.SetActive(false); 
                Time.timeScale = 1f; 
            }
    }

  
}
