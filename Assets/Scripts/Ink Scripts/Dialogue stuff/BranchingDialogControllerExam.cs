using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI; 


public class BranchingDialogControllerExam : MonoBehaviour
{
    
      [SerializeField] private GameObject branchingCanvas; 
    public bool dialogBool = false; 
    public SignalSender DialogEnd; 
    [SerializeField] private GameObject dialogPrefab; 
    [SerializeField] private GameObject choicePrefab; 
    [SerializeField] private TextAssetValue dialogValue; 
    [SerializeField] private Story mystory; 
    [SerializeField] private GameObject dialogHolder; 
    [SerializeField] private GameObject choiceHolder; 
    [SerializeField] private ScrollRect dialogScroll; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableCanvas()
    {
            branchingCanvas.SetActive(true);
            SetStory();
             RefreshView();
    }
    public void DisableCanvas()
    {
         branchingCanvas.SetActive(false);
    }
    public void SetStory()
    {
        if(dialogValue.value)
        {
            DeleteOldDIalogs();
            mystory = new Story(dialogValue.value.text);
        }
        else
        {
            Debug.Log("error with story setup");
        }
    }

    public void DeleteOldDIalogs()
    {
        for(int i = 0; i< dialogHolder.transform.childCount; i++)
        {
            Destroy(dialogHolder.transform.GetChild(i).gameObject);
        }
    }
    public void RefreshView()
    {
       
        while(mystory.canContinue)
        {
            makeNewDialog(mystory.Continue());
        }
        if(mystory.currentChoices.Count>0)
        {
            makeNewChoices(); 
            Debug.Log(" " + mystory.currentChoices.Count);
            
        }
        else
        {
            branchingCanvas.SetActive(false);
            DialogEnd.Raise();
            
        }
    StartCoroutine(ScrollCo());
    }

    IEnumerator ScrollCo()
    {
        yield return null; 
        dialogScroll.verticalNormalizedPosition = 0f;

    }

    void makeNewDialog(string newDialog)
    {
        DialogObject newDialogObject = Instantiate(dialogPrefab, dialogHolder.transform).GetComponent<DialogObject>();
        newDialogObject.Setup(newDialog);
    }

    void makeNewResponse(string newDialog, int choiceValue)
    {
        DialogeButton newResponseObject = Instantiate(choicePrefab, choiceHolder.transform).GetComponent<DialogeButton>();
        newResponseObject.Setup(newDialog, choiceValue);
        Button responseButton = newResponseObject.gameObject.GetComponent<Button>();

        if(responseButton)
        {
            responseButton.onClick.AddListener(delegate{ChooseChoice(choiceValue);});
        } 
    }
    void makeNewChoices()
    {
        for(int i = 0; i <choiceHolder.transform.childCount; i++)
        {
            Destroy(choiceHolder.transform.GetChild(i).gameObject);
        }
        for(int i = 0; i <mystory.currentChoices.Count; i ++)
        {
            makeNewResponse(mystory.currentChoices[i].text, i);
        }
    }

    public void ChooseChoice(int choice)
    {
        mystory.ChooseChoiceIndex(choice); 
        RefreshView();
    }
}
