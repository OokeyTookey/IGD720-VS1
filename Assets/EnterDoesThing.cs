using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDoesThing : MonoBehaviour
{
    public DialogueController2 controller;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            controller.NextSentence();
            this.gameObject.SetActive(false);
        }
    }
}
