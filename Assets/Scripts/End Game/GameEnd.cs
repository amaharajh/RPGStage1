using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameEnd : MonoBehaviour
{
    [SerializeField] private GameObject fadeInPanel;
    [SerializeField] private GameObject fadeoutPanel; 
    [SerializeField] private CalendarData currTime; 
    [SerializeField] private float fade_wait; 
    [SerializeField] private string sceneToLoad; 
    private  PlayerMovement Player; 
    public string exitspawnName;



    public void EndGameScene()
    {

        
        PlayerMovement.spawnPointName = exitspawnName;
        currTime.hh += 4; 
        StartCoroutine(FadeCo());
        
        
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

