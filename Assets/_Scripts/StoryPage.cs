using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryPage : MonoBehaviour
{
    public string[] sentenceArray;
    public TextMeshPro textBox;

    private void Start()
    {
        for (int i = 0; i < sentenceArray.Length -1; i++)
        {
        Debug.Log(sentenceArray[i]);

        }
        textBox = GetComponent<TextMeshPro>();  
    }
}
