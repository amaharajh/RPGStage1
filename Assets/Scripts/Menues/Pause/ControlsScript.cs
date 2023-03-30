using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsScript : MonoBehaviour
{

    public bool controlspaused; 
    public GameObject controlsPanel; 
      public GameObject pausePanel;

    public void ChangeControlsPanelState()
    {
        controlspaused = !controlspaused; 
        if(controlspaused)
            {
                controlsPanel.SetActive(true);
                pausePanel.SetActive(false); 
               
            }
            else 
            {
                controlsPanel.SetActive(false); 
                pausePanel.SetActive(true);
                
            }
    }
}

