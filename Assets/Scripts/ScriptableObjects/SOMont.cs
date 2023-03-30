using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Month", menuName = "ScriptableObjects/MonthUnit")]
[System.Serializable]
public class SOMont : ScriptableObject
{
	public string monthName;
	public int dayAmmount;
	public int monthPos;
	public string StartingDay; 

}
