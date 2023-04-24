using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BigBoyDialogue : MonoBehaviour
{
    public Button rightBtn;
    Animator rightBtnAnim;

    public DialogueController2[] controllers;
    public bool[] chapterFinished;
    int currentPage;
    int finalPage;
    bool fadeInRightBtn = false;
    public float typingSpeed;

    float waitForNextSceneLoadTimer;
    float waitForNextSceneTime = 4;
    bool doOnce = false;
    bool doOnce2 = false;


    private void Start()
    {
        controllers = GetComponentsInChildren<DialogueController2>();

        finalPage = controllers.Length;
        rightBtnAnim = rightBtn.GetComponent<Animator>();
        chapterFinished = new bool[controllers.Length];

        for (int i = 0; i < controllers.Length; i++)
        {
            chapterFinished[i] = controllers[i].finishedAllDialogue;
            chapterFinished[i] = false; //Set all the dialogue to false in the begining.
        }

        controllers[currentPage].StartCoroutine(controllers[currentPage].TypingLetters());
    }

    private void Update()
    {
        waitForNextSceneLoadTimer += Time.deltaTime;

        if (controllers[currentPage].currentPageNumber >= controllers[currentPage].chapters.Length - 1)
        {
            if (!fadeInRightBtn && controllers[currentPage].levelCompleted)
            {
                rightBtnAnim.SetTrigger("PageComplete");
                fadeInRightBtn = true;
            }
        }
        

        if (controllers[currentPage].finishedAllDialogue)
        {
            if (!controllers[currentPage].finalChapter)
            {
                fadeInRightBtn = false;
                currentPage++;
                controllers[currentPage].StartCoroutine(controllers[currentPage].TypingLetters());
                rightBtnAnim.SetTrigger("Disabled");
                chapterFinished[currentPage] = false;
            }
           
            if (controllers[currentPage].finalChapter && !doOnce)
            {
                waitForNextSceneLoadTimer = 0;
               fadeInRightBtn = false;
                rightBtnAnim.SetTrigger("Disabled");
                doOnce = true;
            }

            if (waitForNextSceneLoadTimer >= waitForNextSceneTime && doOnce && !doOnce2)
            {
                SceneManager.LoadScene(controllers[currentPage].nextSceneName);
                doOnce2 = true;
            }
        }
    }

    public void PageFlip()
    {
        controllers[currentPage].PageFlip();
    }

    public void LoadBlankPage()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
