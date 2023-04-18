using UnityEngine;
using System.Collections;
using TMPro;

public class DialogueController2 : MonoBehaviour
{
  
    [HideInInspector] public int currentPageNumber;
    private bool finishedSentence;

    public float typingSpeed;

    public StoryPage[] chapters;


    private void Start()
    {
        currentPageNumber= 0;
       StartCoroutine(TypingLetters());


    }





    private void Update()
    {
        if (chapters[currentPageNumber].textBox.text == chapters[currentPageNumber].sentence)
        {
            Debug.Log("I think i ahve done it");
            finishedSentence = true;
        }

        if (Input.GetButton("Submit") && finishedSentence == true)
        {
            NextPage();
        }      
    }

    public void NextPage()
    {
        Debug.Log("doing stuff");
        
            currentPageNumber++;
            StartCoroutine(TypingLetters());
            finishedSentence = false;

    }

    public IEnumerator TypingLetters()
    {
        //Foreach will allow us to access a specfic variable type in statements. IE: Each letter in a sentence.
        foreach (var letter in chapters[currentPageNumber].sentence) //ToCharArray copies the chars and put them into unicode (readable)
        {
            chapters[currentPageNumber].textBox.text += letter; //access the TexhMeshPro object then add a letter everytime the coroutine runs.
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}