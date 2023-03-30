using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class MainMenu : MonoBehaviour
{

    public GameSaveManager gamesaver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewGame()
    {
        gamesaver.ResetScriptables();
        SceneManager.LoadScene("Home");
        
    }
    public void LoadGame()
    {
        gamesaver.LoadScriptables();
        SceneManager.LoadScene("Home");
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial"); 
    }
}
