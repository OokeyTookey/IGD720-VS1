using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    Animator animator;
    BoxCollider2D boxCollider;
    [HideInInspector] public bool misisonComplete;
    bool openedChest = false;
    public DialogueController2 dialogueController;
    SpriteRenderer parentSpriteRenderer;

    void Start()
    {
        animator = this.GetComponentInParent<Animator>();
        boxCollider = this.GetComponent<BoxCollider2D>();
        parentSpriteRenderer = this.GetComponentInParent<SpriteRenderer>();
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
            StartCoroutine(FadeOut(1.5f, parentSpriteRenderer));
            boxCollider.enabled = false;
            openedChest = true;
            dialogueController.NextSentence();
        }
    }

    public IEnumerator FadeOut(float time, SpriteRenderer image)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        while (image.color.a > 0.0f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (Time.deltaTime / time));
            yield return null;
        }
    }
}
