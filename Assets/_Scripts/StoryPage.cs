using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryPage : MonoBehaviour
{
    public string sentence;
    public TextMeshProUGUI textBox;
    public bool autoStart = false;

    private void Start()
    {
        textBox = this.GetComponent<TextMeshProUGUI>();  
    }
}
