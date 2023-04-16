using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChapterManager : MonoBehaviour
{
    public PageSweeper[] chapters;
    public PageSweeper[] allPages;
    public CharacterWobble charWobble;
    int currentChapter;
    
    void Start()
    {
        currentChapter= 0;
        //allPages = FindObjectsOfType<PageSweeper>();
        //ClearAllChapters();
    }

    public void ClearAllChapters()
    {
        for (int i = 0; i < allPages.Length; i++)
        {
            allPages[i].PageSweeperClear();
        }  
    }

    public void NewChapter()
    {
        currentChapter++;
        chapters[currentChapter].gameObject.SetActive(true); //Potentially set them as deactive.
        chapters[currentChapter - 1].PageSweeperClear();
        chapters[currentChapter - 1].gameObject.SetActive(false); //Potentially set them as deactive.
      //  charWobble.SetText(chapters[currentChapter - 1].childDialogues[currentChapter - 1].text);
    }

    public void PreviousChapter()
    {
        chapters[currentChapter].gameObject.SetActive(false); //Potentially set them as deactive.

        currentChapter--;
        chapters[currentChapter].gameObject.SetActive(true); //Potentially set them as deactive.
        chapters[currentChapter].PageSweeperRestore();
    }

   
}
