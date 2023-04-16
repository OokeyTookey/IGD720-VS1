using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [HideInInspector] public int index;
    private bool finishedSentence;

    public float typingSpeed;
    public float typingSpeedMultipler;
    public string[] dialogueArray;

    public TextMeshProUGUI textBox;

    private void Start()
    {
        StartCoroutine(TypingLetters());
    }

    private void Update()
    {
        //Checks if the entire sentence has been printed, if so, set the bool to true.
        if (textBox.text == dialogueArray[index])
        {
            finishedSentence = true;
        }

        if (Input.GetButton("Submit") && finishedSentence == true)
        {
            finishedSentence = false;
            Next();
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
            this.gameObject.SetActive(false);
        }
    }
}
