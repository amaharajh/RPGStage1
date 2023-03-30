using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BedTransition : Interactable
{
    public CalendarData calendarData; 
    public string sceneToLoad; 
    public GameObject fadeInPanel;
    public GameObject fadeoutPanel; 
    public float fade_wait; 
    public Text dialogText; 
    public GameObject dialogBox; 
    public string exitspawnName; 
    // Start is called before the first frame update
    private void Awake()
    {
        if(fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&& playerInRange && calendarData.hh >19){
          
            StartCoroutine(FadeCo());
        } 
    }

       public override void OnTriggerEnter2D (Collider2D other)
    {

        if(other.CompareTag("Player_Passive") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = true;  
            dialogBox.SetActive(true);
            if(calendarData.hh >= 8 && calendarData.hh <= 10)
            {
                dialogText.text = "I should get ready to head to class...maybe a few more min- NO...";
            }
             if(calendarData.hh > 10 && calendarData.hh <= 14)
            {
                dialogText.text = "Shouldn't I be in class???...";
            }
            if(calendarData.hh > 14 && calendarData.hh <= 19)
            {
                dialogText.text = "I'm not feeling that tired as yet...."; 
            }
            else if (calendarData.hh > 19 )
            {
                dialogText.text = "It's pretty late..I should go to bed";
                PlayerMovement.spawnPointName = exitspawnName;             
            }
             
                
            
        }
    }

    public override void OnTriggerExit2D(Collider2D  other)
    {

        if (other.CompareTag("Player_Passive") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = false; 
            dialogBox.SetActive(false);
        
        }
    }

    
    private  IEnumerator FadeCo()
    {
        if (fadeoutPanel != null)
        {
        Instantiate(fadeoutPanel, Vector3.zero, Quaternion.identity); 
        }
        yield return new WaitForSeconds(fade_wait);

        calendarData.hh = 8;
        if(calendarData.maxMonthDays == calendarData.date)
        {
            calendarData.date = 1; 
            
            if(calendarData.actualMonth == 11)
            {
                calendarData.actualMonth = 0; 
                calendarData.yy++;
            }
            else
            calendarData.actualMonth++; 
        }
        else
        calendarData.date+=1; 
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while(!asyncOperation.isDone)
        {
            yield return null; 
        }
    }
}
