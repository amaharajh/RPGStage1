using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class SceneTransition : MonoBehaviour
{

    public string sceneToLoad; 
    public GameObject fadeInPanel; 
    public GameObject fadeOutPanel; 
    public float fadeWait;
    public string exitspawnName; 

  

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if((other.CompareTag("Player") ||other.CompareTag("Player_Passive") )&& !other.isTrigger)
        {
           // playerStorage.initialValue = playerPosition; 
           PlayerMovement.spawnPointName = exitspawnName; 
            StartCoroutine(FadeCo());
            //SceneManager.LoadScene(sceneToLoad);
        }
    }

    public IEnumerator FadeCo()
    {
        if (fadeOutPanel != null)
        {
        Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity); 
        }
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while(!asyncOperation.isDone)
        {
            yield return null; 
        }
    }
        
}
