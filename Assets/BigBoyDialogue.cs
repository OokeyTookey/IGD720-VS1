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


    private void Start()
    {
        controllers = GetComponentsInChildren<DialogueController2>();
        Debug.Log(controllers.Length);

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
        if (controllers[currentPage].currentPageNumber >= controllers[currentPage].chapters.Length - 1)
        {
            if (!fadeInRightBtn)
            {
                //Debug.Log("HIIYYYAAH");
                rightBtnAnim.SetTrigger("PageComplete");
                fadeInRightBtn = true;
            }
        }
        

        if (controllers[currentPage].finishedAllDialogue)
        {
            fadeInRightBtn = false;
            Debug.Log(controllers[currentPage]);
            currentPage++;
            controllers[currentPage].StartCoroutine(controllers[currentPage].TypingLetters());
            rightBtnAnim.SetTrigger("Disabled");
            chapterFinished[currentPage] = false;
        }

        if (currentPage >= finalPage)
        {
            LoadBlankPage();
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
