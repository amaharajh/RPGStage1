using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class ClassToHomeTransition : SceneTransition
{
    [SerializeField] private CalendarData currTime;
    // Start is called before the first frame update

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if((other.CompareTag("Player") ||other.CompareTag("Player_Passive") )&& !other.isTrigger)
        {
           // playerStorage.initialValue = playerPosition; 
           currTime.hh += 1; 
           PlayerMovement.spawnPointName = exitspawnName; 
            StartCoroutine(FadeCo());
            //SceneManager.LoadScene(sceneToLoad);
        }
    }

}
