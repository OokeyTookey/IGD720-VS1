using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class DialogueController2 : MonoBehaviour
{
    BigBoyDialogue parentController;
    
    [HideInInspector] public int currentPageNumber;
    private bool finishedSentence;

    public StoryPage[] chapters;

    private float letterFadeOutTime = 0.2f;
    private float letterFadeInTime = 1f;

    [HideInInspector] public bool finishedPage = false;

    [HideInInspector] public bool doOnce = false;
    public bool finishedAllDialogue = false;

    bool pageFlipped;
    float fadeOutTimer;
    float fadeOutMax = 2;

    public bool finalChapter;
    public string nextSceneName;

    public bool levelCompleted;

    private void Start()
    {
        parentController = FindObjectOfType<BigBoyDialogue>();
        currentPageNumber = 0;
        chapters = GetComponentsInChildren<StoryPage>();
    }

    private void Update()
    {

        fadeOutTimer += Time.deltaTime;
        if (chapters[currentPageNumber].textBox.text == chapters[currentPageNumber].sentence)
        {
            finishedSentence = true;

            if (chapters[currentPageNumber].autoStartNextSentence)
            {
                NextSentence();
            }
        }

        if (pageFlipped && fadeOutTimer > fadeOutMax)
        {
            finishedAllDialogue = true;
        } 


        if (Input.GetButton("Submit") && finishedSentence == true)
        {
            NextSentence();
        }
    }

    public void PageFlip()
    {
        fadeOutTimer = 0;
        PageSweeperClear();
        pageFlipped= true;
    }


    public void NextSentence()
    {
        if (!finishedPage)
        {
            currentPageNumber++;
            StartCoroutine(TypingLetters());
            finishedSentence = false;
        }    
    }

    public void StartSentence()
    {
        if (!finishedPage)
        {
            StartCoroutine(TypingLetters());
            finishedSentence = false;
        }
    }

    public IEnumerator TypingLetters() //Add typewrier effect
    {
        //Foreach will allow us to access a specfic variable type in statements. IE: Each letter in a sentence.
        foreach (var letter in chapters[currentPageNumber].sentence) //ToCharArray copies the chars and put them into unicode (readable)
        {
            chapters[currentPageNumber].textBox.text += letter; //access the TexhMeshPro object then add a letter everytime the coroutine runs.
            yield return new WaitForSeconds(parentController.typingSpeed);
        }
    }

    //------------------------------ Page Sweeper ----------------------------------------


    public void PageSweeperClear()
    {
        for (int i = 0; i < chapters.Length; i++)
        {
            StartCoroutine(FadeOut(letterFadeOutTime, chapters[i].textBox));
        }
    }

    public void EmptyPages()
    {
        for (int i = 0; i < chapters.Length; i++)
        {
            chapters[i].textBox.text = "";
        }
    }

    public IEnumerator FadeOut(float time, TMP_Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / time));
            yield return null;
        }
    }

    public void PageSweeperFadeIn()
    {
        for (int i = 0; i < chapters.Length; i++)
        {
            StartCoroutine(FadeIn(letterFadeInTime, chapters[i].textBox));
        }
    }
    public IEnumerator FadeIn(float time, TMP_Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / time));
            yield return null;
        }
    }
}