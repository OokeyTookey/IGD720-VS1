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
 public BigBoyDialogue parentController;
    bool doOnce;

    void Start()
    { 
        parentController = FindAnyObjectByType<BigBoyDialogue>();    
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

        if (misisonComplete && !doOnce)
        {
          
            //doOnce = true;
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
            animator.SetTrigger("FadeAway");

           // StartCoroutine(FadeOut(1.5f, parentSpriteRenderer));
            Debug.Log("open ttreasure");
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
