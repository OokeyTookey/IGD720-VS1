using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class PageSweeper : MonoBehaviour
{
    public GameObject parentDialogues;
    private TMP_Text[] childDialogues;
    int index;
    public float letterFadeOutTime = 0.6f;

    private void Start()
    {
        index = 0;

        var children = parentDialogues.GetComponentsInChildren<TMP_Text>();
        childDialogues = children;

        foreach (var text in children)
        {
            childDialogues[index]= text;
            if (index >= childDialogues.Length)
            {
                break;
            }
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


    public IEnumerator FadeOut(float time, TMP_Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / time));
            yield return null;
        }
    }
}

