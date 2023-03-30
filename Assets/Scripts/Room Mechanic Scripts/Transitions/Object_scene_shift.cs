using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Object_scene_shift : Interactable
{
    public string sceneToLoad; 
    public GameObject fadeInPanel;
    public GameObject fadeoutPanel; 
    public float fade_wait; 
    public Text dialogText; 
     public GameObject dialogBox; 
    public string dialog; 
    private  PlayerMovement Player; 
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
        if(Input.GetKeyDown(KeyCode.Space)&& playerInRange){
          
            StartCoroutine(FadeCo());
        } 
    }

   public override void OnTriggerEnter2D (Collider2D other)
    {

        if(other.CompareTag("Player_Passive") && !other.isTrigger)
        {
            context.Raise();
            PlayerMovement.spawnPointName = exitspawnName; 
            playerInRange = true;  
             dialogBox.SetActive(true);
             dialogText.text = dialog; 
                
            
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
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while(!asyncOperation.isDone)
        {
            yield return null; 
        }
    }
}

