using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseAssignmentsMenu : DeadlineMenu
{

    protected override void Update()
    {
        if(Input.GetButtonDown("courseAssignments"))
        {
            ChangePauseState();
        }
    }

}
