using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    Animator animator;
    BoxCollider2D boxCollider;
    [HideInInspector] public bool misisonComplete;
    bool openedChest = false;
    public DialogueController2 dialogueController;

    void Start()
    {
        animator = this.GetComponentInParent<Animator>();
        boxCollider = this.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (openedChest && !misisonComplete)
        {
            dialogueController.levelCompleted = true;
            misisonComplete = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckForPlayer(collision);
    }

    private void CheckForPlayer(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Treasure");
            boxCollider.enabled = false;
            openedChest = true;
            dialogueController.NextSentence();
        }
    }
}
