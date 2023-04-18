using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueSystem : MonoBehaviour
{
    public int pageIndex = 0;
    public GameObject[] DialogueControllers;
    public TMP_Text[] childDialogues;


    public TMP_Text[] childDialoguesSaved;

    TextMeshPro currentText;

    public float typingSpeed;

    TMP_Text[] currentDialogue;
    public TextMeshProUGUI[] currentUIBox;


    private void Start()
    {
        for (int i = 0; i < DialogueControllers.Length; i++)
        {
            DialogueControllers[i].GetComponentsInChildren<TMP_Text>();
           // currentDialogue[i] = DialogueControllers[i].GetComponent<TMP_Text>();

            Debug.Log(DialogueControllers[i].name);
        }

        //currentDialogue = DialogueControllers[pageIndex].GetComponentsInChildren<TMP_Text>();

        for (int i = 0; i < DialogueControllers.Length; i++)
        {
        }
        

        childDialoguesSaved = currentDialogue; //Saves the current dialogue to the saved one ()inshallah


        Debug.Log(currentDialogue[pageIndex].text);




       // StartCoroutine(TypingLetters());


        //ClearAllTextBoxes();
        //UpdateCurentDialogueToPageIndex();
    }

    public void ClearAllTextBoxes()
    {

    }

    public void UpdateCurentDialogueToPageIndex()
    {
        //Adds all TMP_TEXT objects which are childed to current Dialog
        currentDialogue = DialogueControllers[pageIndex].GetComponentsInChildren<TMP_Text>();
        currentUIBox = new TextMeshProUGUI[currentDialogue.Length];


        for (int i = 0; i < currentDialogue.Length; i++)//Printing all the children
        {
            currentUIBox[i] = currentDialogue[i].GetComponent<TextMeshProUGUI>();
           // Debug.Log(currentDialogue[i].text);
        }

        //StartCoroutine(TypingLetters());



    }


     public IEnumerator TypingLetters()
    {
        //Foreach will allow us to access a specfic variable type in statements. IE: Each letter in a sentence.
        foreach (var letter in childDialoguesSaved[pageIndex].text) 
        {
            childDialoguesSaved[pageIndex].text += letter; //access the TexhMeshPro object then add a letter everytime the coroutine runs.
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // textInParentObjects = new TMP_Text[parentObjects.Length];

    //grab all tmp and assignment the to text in parent

    //Get curent text box
    //get current dialogue 
    //Pass the string into the typing text coroutine
    //check if done, 
    // if done && space bar pressed, increment nextDialogbox index
    // 




}
