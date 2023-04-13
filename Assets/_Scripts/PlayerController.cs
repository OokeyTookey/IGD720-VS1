using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int swordDamage;
    public SwordAttack swordAttack;

    [SerializeField] float swordColliderOffset;

    Rigidbody2D rb;
    Vector2 movementInput;
    Vector2 currentDirectionFacing;
    Animator animator;
    SpriteRenderer spriteRenderer;

    /*public enum directionsFaced
    {
        up,
        down,
        left,
        right
    }*/

    //public directionsFaced directions;

    float collsionOffset = 0.05f;
    public float movementSpeed;
    [SerializeField] ContactFilter2D attackFilter; //For raycast layers
    [SerializeField] ContactFilter2D movementFilter; //For raycast layers
    List<RaycastHit2D> castedCollisions = new List<RaycastHit2D>();

    private void Start()
    {
       // directions = directionsFaced.down;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartSwordAttack()
    {
        swordAttack.UpdateBoxColliderPosition(currentDirectionFacing / swordColliderOffset);
        swordAttack.enableAttack = true;
    }

    public void EndSwordAttack()
    {
        swordAttack.enableAttack = false;
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero) //If there is no player input
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
        //Debug.Log("The player's current directions is: " + directions);
    }

    public bool AttemptMovement(Vector2 direction) //The script will first check if the player can make a viable move before actually moving
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

    /*void WhatDirectionIsThePlayerFacing()
    {
        if (movementInput.x > 0.01)
        {
            directions = directionsFaced.right;
            //Debug.Log("Right");
        }
        if (movementInput.x < -0.01)
        {
            directions = directionsFaced.left;
            // Debug.Log("Left");
        }
        if (movementInput.y > 0.01)
        {
            directions = directionsFaced.up;
            // Debug.Log("Up");
        }
        if (movementInput.y < -0.01)
        {
            directions = directionsFaced.down;
            // Debug.Log("Down");
        }
    }*/

    //------------------------------------------------Unity input system functions.

    void OnFire()
    {
        animator.SetTrigger("SwordAttack");
        //TurnOnSwordCollidersBasedOnDirection();

       /* switch (directions)
        {
            case directionsFaced.up:
                //swordAttack.SwordAttackUp();
                //Debug.Log("switchcase up");
                return;
            case directionsFaced.down:
                //swordAttack.SwordAttackDown();
                //Debug.Log("switchcase down");
                return;
            case directionsFaced.left:
                // swordAttack.SwordAttackLeft();
                //Debug.Log("switchcase left");
                return;
            case directionsFaced.right:
                //swordAttack.SwordAttackRight();
                //Debug.Log("switchcase right");
                return;
        }*/
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();

        if (movementInput != Vector2.zero)
            currentDirectionFacing = movementInput;

        //WhatDirectionIsThePlayerFacing(); //Could call this in update, but it makes sense when the player presses the key.
    }
}
