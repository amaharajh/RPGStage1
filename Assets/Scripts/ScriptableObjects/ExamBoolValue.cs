using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class ExamBoolValue : ScriptableObject
{
    // Start is called before the first frame update
    public bool initialValue; 
  
   public bool RuntimeValue;

   public int assignmentCode;
   public int courseCode; 
  
}

