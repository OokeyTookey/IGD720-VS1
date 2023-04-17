using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Swooooord
    public int swordDamage;
    public SwordAttack swordAttack;
    [SerializeField] float swordColliderOffset;
    Vector2 currentDirectionFacing;

    //Player movement
    Vector2 movementInput;
    private Vector2 dampMovement;
    private Vector2 dampMovementVelocity;
    public float turnSpeed;
    public float movementSpeed;

    public float playerHealth;
    bool playerDead;

    Rigidbody2D rb;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!playerDead)
        {
            dampMovement = Vector2.SmoothDamp(
                dampMovement,
                movementInput,
                ref dampMovementVelocity,
                turnSpeed);

            rb.velocity = dampMovement * movementSpeed;

            if (movementInput != Vector2.zero) //If there is player input, this means the animation returns to the idle position.
            {
                animator.SetFloat("Horizontal", movementInput.x);
                animator.SetFloat("Vertical", movementInput.y);
                animator.SetFloat("Speed", movementInput.sqrMagnitude); //Checking the length of the movement value, is it above 0.01?
            }

            if (movementInput == Vector2.zero)
            {
                animator.SetFloat("Speed", 0);
            }
        }
    }

    //------------------------------------------------Sword related tasks.


    public void StartSwordAttack()
    {
        swordAttack.UpdateBoxColliderPosition(currentDirectionFacing / swordColliderOffset);
        swordAttack.enableAttack = true;
    }

    public void EndSwordAttack()
    {
        swordAttack.enableAttack = false;
    }

    public void PlayerDeath()
    {
        playerDead = true;
        animator.SetTrigger("PlayerDeath");

    }

    //------------------------------------------------Unity input system functions.

    void OnFire()
    {
        animator.SetTrigger("SwordAttack");
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();

        if (movementInput != Vector2.zero)
        {
            currentDirectionFacing = movementInput;
        }
    }
}
