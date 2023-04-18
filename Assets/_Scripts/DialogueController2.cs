using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class DialogueController2 : MonoBehaviour
{
    [HideInInspector] public int currentPageNumber;
    private bool finishedSentence;

    public float typingSpeed;
    public StoryPage[] chapters;
    //public List<StoryPage> chapters;

    private float letterFadeOutTime = 0.6f;
    private float letterFadeInTime = 1f;

    bool finishedPage = false;

    public Animator rightButtonAnimator;

    int currentChapterLength;
    public string NextScene;
    bool doOnce = false;
    public bool finishedAllDialogue = false;

    private void Start()
    {
        currentPageNumber = 0;
        chapters = GetComponentsInChildren<StoryPage>();
        StartCoroutine(TypingLetters());

    }

    private void Update()
    {
        if (chapters[currentPageNumber].textBox.text == chapters[currentPageNumber].sentence)
        {
            finishedSentence = true;

            if (chapters[currentPageNumber].autoStart)
            {
                NextSentence();
            }
        }

        if (chapters[currentPageNumber].name.Contains("NEXT"))
        {
            finishedPage = true;
            if (finishedPage && !doOnce)
            {
                rightButtonAnimator.SetTrigger("PageComplete");
                doOnce = true;
            }
        }


        if (Input.GetButton("Submit") && finishedSentence == true)
        {
            NextSentence();
        }
    }

    public void PageFlip()
    {
        rightButtonAnimator.SetTrigger("Disabled");
        PageSweeperClear();
      // finishedAllDialogue = true;

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

    public IEnumerator TypingLetters() //Add typewrier effect
    {
        //Foreach will allow us to access a specfic variable type in statements. IE: Each letter in a sentence.
        foreach (var letter in chapters[currentPageNumber].sentence) //ToCharArray copies the chars and put them into unicode (readable)
        {
            chapters[currentPageNumber].textBox.text += letter; //access the TexhMeshPro object then add a letter everytime the coroutine runs.
            yield return new WaitForSeconds(typingSpeed);
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