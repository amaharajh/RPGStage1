using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamCheck : MonoBehaviour
{
    [SerializeField] private ExamDoor DoorInfo; 
    [SerializeField] private ExamBoolValue[] ExamSlip; 
    // Start is called before the first frame update
    
    public void HandingExamPaper()
    {
        for(int i = 0; i < DoorInfo.courseinfo.Length; i++)
        {
            if(ExamSlip[i].assignmentCode == DoorInfo.courseinfo[i].CourseAssignmentCode)
            {
                ExamSlip[i].RuntimeValue = true; 
            }
            else
            {
                Debug.Log("Slips do not match the examination");
            }
        }
    }
}
