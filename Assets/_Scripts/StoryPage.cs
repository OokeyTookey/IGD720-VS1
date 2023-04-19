using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryPage : MonoBehaviour
{
    public string sentence;
    public TextMeshProUGUI textBox;
    public bool autoStartNextSentence = false;

    private void Awake()  //Has to be awake because dialogue controller stuff happens in start and there gets null errors.
    {
        textBox = this.GetComponent<TextMeshProUGUI>(); 
    }
}
