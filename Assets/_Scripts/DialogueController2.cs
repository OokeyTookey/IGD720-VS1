using UnityEngine;
using System.Collections;
using TMPro;

public class DialogueController2 : MonoBehaviour
{
  
    [HideInInspector] public int index;
    private bool finishedSentence;

    public float typingSpeed;
    public string[] dialogueArray;

    public TextMeshPro textBox;


    public StoryPage[] chapters;


    private void Start()
    {
       // Debug.Log(chapters[0].name);
       // Debug.Log(chapters[1].name);
       //
       // Debug.Log(chapters[0].sentenceArray);
    }


    private void Update()
    {
        /*if (textBox.text == dialogueArray[index])
        {
            finishedSentence = true;
        }

        if (Input.GetButton("Submit") && finishedSentence == true)
        {
            finishedSentence = false;
            Next();
        }       */
    }

    public void NextSentence()
    {
        if (finishedSentence == true)
        {
            Next();
            finishedSentence = false;
        }
    }

    public IEnumerator TypingLetters()
    {
        //Foreach will allow us to access a specfic variable type in statements. IE: Each letter in a sentence.
        foreach (var letter in dialogueArray[index].ToCharArray()) //ToCharArray copies the chars and put them into unicode (readable)
        {
            textBox.text += letter; //access the TexhMeshPro object then add a letter everytime the coroutine runs.
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void Next()
    {
        if (index < dialogueArray.Length - 1) //Check if the index is at the end of the story arc
        {
            index++;
            textBox.text = ""; //Resets the text to blank
            StartCoroutine(TypingLetters());
        }

        else //If there is no story left, set to blank N disable
        {
            textBox.text = "";
        }
    }
}