using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class PageSweeper : MonoBehaviour
{
    public GameObject[] parentDialogues;
    private TMP_Text[] childDialogues;

    private void Start()
    {
        Debug.Log("Number of Master Dialogues in the scene: " + parentDialogues.Length);
        for (int i = 0; i < parentDialogues.Length; i++)
        {
           // childDialogues[i] = parentDialogues[i].
        }
    }
}
