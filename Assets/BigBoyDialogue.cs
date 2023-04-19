using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigBoyDialogue : MonoBehaviour
{
    public Button rightBtn;
    Animator rightBtnAnim;

    public DialogueController2[] controllers;
    public bool[] chapterFinished;
    int currentPage;
    bool fadeInRightBtn = false;

    private void Start()
    {
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
        if (controllers[currentPage].currentPageNumber >= controllers[currentPage].chapters.Length - 1)
        {
            Debug.Log("doing something");
            if (!fadeInRightBtn)
            {
                rightBtnAnim.SetTrigger("PageComplete");
                fadeInRightBtn = true;
            }
        }
        

        if (controllers[currentPage].finishedAllDialogue)
        {
            Debug.Log(controllers[currentPage]);
           // controllers[currentPage].EmptyPages();
            currentPage++;
            controllers[currentPage].StartCoroutine(controllers[currentPage].TypingLetters());
            rightBtnAnim.SetTrigger("Disabled");
            chapterFinished[currentPage] = false;
        }
    }

    public void PageFlip()
    {
        controllers[currentPage].PageFlip();
    }
}
