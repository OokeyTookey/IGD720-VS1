using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movementInput;
    Animator animator;
    SpriteRenderer spriteRenderer;


    public float movementSpeed;
    public float collsionOffset = 0.05f;
    public ContactFilter2D movementFilter; //For raycast layers
    List<RaycastHit2D> castedCollisions = new List<RaycastHit2D>();


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            //The player cannot move diag if there is a collision in the way
            //So, we need to check the x and y direction to see if there is a viable movement

            bool successfulMovement = AttemptMovement(movementInput);
            if (!successfulMovement)
            {
                successfulMovement = AttemptMovement(new Vector2(movementInput.x, 0));
                if (!successfulMovement)
                {
                    AttemptMovement(new Vector2(0, movementInput.y));
                }
            }
            animator.SetFloat("Horizontal", movementInput.x);
            animator.SetFloat("Vertical", movementInput.y);
            animator.SetFloat("Speed", movementInput.sqrMagnitude); //Checking the length of the movement value, is it above 0.01?
        }

        if (movementInput == Vector2.zero)
        {
            animator.SetFloat("Speed", 0);
        }
    }

    private void PlayerAnimationDirection(string AnimationName, bool TrueRFalse)
    {

    }

    private bool AttemptMovement(Vector2 direction)
    {
        //theRay creates a new ray from the player can checks if the move is valid, aka, if the player collide with anything
        //theRay will return a value, if the value is 0, AKA, No collsions, it will move the player.
        int theRay = rb.Cast(direction, movementFilter, castedCollisions, movementSpeed * Time.fixedDeltaTime + collsionOffset); 
        if (theRay == 0) //No collisions detected
        {
            rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime);
            return true; //if the movement occured
        }
        else
        {
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
