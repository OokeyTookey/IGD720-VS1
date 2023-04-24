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
    float waitForNextSceneTime = 2;
    bool doOnce = false;
    bool doOnce2 = false;
    public Animator playerAnim;


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
        rightBtn.interactable = false;

    }

    private void Update()
    {
        waitForNextSceneLoadTimer += Time.deltaTime;

        if (controllers[currentPage].currentPageNumber >= controllers[currentPage].chapters.Length - 1)
        {
            if (!fadeInRightBtn && controllers[currentPage].levelCompleted)
            {
                rightBtnAnim.SetTrigger("PageComplete");
                rightBtn.interactable = true;
                fadeInRightBtn = true;
            }
        }
        

        if (controllers[currentPage].finishedAllDialogue)
        {
            if (!controllers[currentPage].finalChapter)
            {
                fadeInRightBtn = false;
                chapterFinished[currentPage] = true;
                currentPage++;
                controllers[currentPage].StartCoroutine(controllers[currentPage].TypingLetters());
                rightBtnAnim.SetTrigger("Disabled");
                rightBtn.interactable = false;
                chapterFinished[currentPage] = false;
            }

            if (controllers[currentPage].finalChapter && !doOnce)
            {
                waitForNextSceneLoadTimer = 0;
                fadeInRightBtn = false;
                rightBtnAnim.SetTrigger("Disabled");
                rightBtn.interactable = false;
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

    public void FadePlayerOut()
    {
        playerAnim = FindAnyObjectByType<PlayerController>().GetComponent<Animator>();
        playerAnim.SetTrigger("FadePlayerOut");
    }
}
