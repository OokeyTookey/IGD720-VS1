using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChapterManager : MonoBehaviour
{
    public GameObject[] chapterMaster;
    PageSweeper[] chapters;

    //public CharacterWobble charWobble;
    int currentChapter;
    
    void Start()
    {
        currentChapter= 1;
        chapters = new PageSweeper[chapterMaster.Length];

        for (int i = 0; i < chapterMaster.Length; i++)
        {
            chapters[i] = chapterMaster[i].GetComponent<PageSweeper>();
            chapterMaster[i].SetActive(false);
        }
       NewChapter(); //Starts the game by looking for the 2nd chapter.
    }

    /*public void NewChapter()
    {
        if (currentChapter > -1 && currentChapter <= chapterMaster.Length)
        {
            chapterMaster[currentChapter].SetActive(true); //dont touch it works
            //chapterMaster[currentChapter-1].SetActive(true);

            chapters[currentChapter].PageSweeperRestore();
            //chapters[currentChapter].PageSweeperClear();
           // chapterMaster[currentChapter - 1].SetActive(false);
            currentChapter++;

        }
    } */
    
    public void NewChapter()
    {
        if (currentChapter > -1 && currentChapter <= chapterMaster.Length)
        {
            chapterMaster[currentChapter - 1].SetActive(false); //dont touch it works
            chapterMaster[currentChapter].SetActive(true); //dont touch it works
            chapters[currentChapter].PageSweeperFadeIn(); //dont touch it works
            currentChapter++;
        }
    }

    public void PreviousChapter()
    {
        currentChapter--;
        chapters[currentChapter].PageSweeperFadeIn();
    }
}
