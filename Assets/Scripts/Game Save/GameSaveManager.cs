using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; 

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager gameSave; 

    public List<ScriptableObject> Objects = new List<ScriptableObject>(); 

    public void ResetScriptables()
    {
        Debug.Log("Resetting Scriptables");
        for (int i = 0; i < Objects.Count; i++)
        {
          
            switch (Objects[i].GetType().FullName)
            {
                case "FloatValue":
                    FloatValue ftmp = (FloatValue)Objects[i];
                    ftmp.RuntimeValue = ftmp.initialValue;
                    Debug.Log("resetting FloatValue");
                    break;

                case "BoolValue":
                    BoolValue btmp = (BoolValue)Objects[i];
                    btmp.RuntimeValue = btmp.initialValue;
                    Debug.Log("resetting BoolValue");
                    break;

                case "Inventory":
                    Inventory itmp = (Inventory)Objects[i];
                    itmp.CopperKeys = 0;
                    itmp.SilverKeys = 0;
                    itmp.GoldKeys = 0;
                    itmp.CompSysCoins = 0;
                    itmp.CommSysCoins = 0;
                    itmp.ControlSysCoins = 0;
                    itmp.ElecPowerCoins = 0;
                    itmp.AccSysCoins = 0;
                    Debug.Log("Inventory Reset");
                    break;

                case "CalendarData":
                    CalendarData clmp = (CalendarData)Objects[i];

                    clmp.mm = clmp.initial_mm;
                    clmp.hh = clmp.initial_hh;
                    clmp.date = clmp.initial_date;
                    clmp.actualMonth = clmp.initial_actualMonth;
                    clmp.yy = clmp.initial_yy;
                    Debug.Log("resetting CalendarData");
                    break;
                    
                case "Grades":
                    Grades grmp = (Grades)Objects[i]; 
                        grmp.RuntimeValue = grmp.initialValue; 
                        Debug.Log("Grades Reset");
                        break; 
                
                  

                default:
                    break;
            }
            if(File.Exists(Application.persistentDataPath +
                string.Format($"/{i}.dat")))
            {
                File.Delete(Application.persistentDataPath +
                string.Format($"/{i}.dat"));
            }
        }
        
    }
    private void OnEnable()
    {
        LoadScriptables();
    }
    private void OnDisable()
    {
        SaveScriptables();
    }
    
   public void SaveScriptables()
    {
        Debug.Log("Saving to: " + Application.persistentDataPath);

        for (int i = 0; i < Objects.Count; i++)
        {
            //open a file
            FileStream file = File.Create(Application.persistentDataPath +
                string.Format($"/{i}.dat"));

            //create a binary formatter
            BinaryFormatter binary = new BinaryFormatter();

            //format the object as json
            var json = JsonUtility.ToJson(Objects[i]);

            //write to the file
            binary.Serialize(file, json);

            //close the file
            file.Close();
        }
    }
    public void LoadScriptables()
    {
        
        for (int i = 0; i < Objects.Count; i++)
        {

            if (File.Exists(Application.persistentDataPath +
                string.Format($"/{i}.dat")))
            {
                FileStream file = File.Open(Application.persistentDataPath +
                string.Format($"/{i}.dat"), FileMode.Open);

                BinaryFormatter binary = new BinaryFormatter();

                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file),
                    Objects[i]);

                file.Close();

            }


        }

    }
}
