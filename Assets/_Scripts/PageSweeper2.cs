using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class PageSweeper2 : MonoBehaviour
{
   // public GameObject[] bigDaddyDialogue;
    private GameObject parentDialogues;
    public TMP_Text[] childDialogues;
    int index;
    private float letterFadeOutTime = 0.6f;
    private float letterFadeInTime = 1f;

    private void Start()
    {
        parentDialogues = this.gameObject;
        index = 0;

        var children = parentDialogues.GetComponentsInChildren<TMP_Text>();
        childDialogues = children;
      
            foreach (var text in children)
            {
                childDialogues[index] = text;
                if (index >= childDialogues.Length)
                {
                    break;
                }
               //Debug.Log(children[index].text);
                index++;
            }
    }

    public void PageSweeperClear()
    {
        for (int i = 0; i < childDialogues.Length; i++)
        {
            StartCoroutine(FadeOut(letterFadeOutTime, childDialogues[i]));
        }
    }

    public void PageSweeperFadeIn()
    {
        for (int i = 0; i < childDialogues.Length; i++)
        {
            StartCoroutine(FadeIn(letterFadeInTime, childDialogues[i]));
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

