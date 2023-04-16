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

    float collsionOffset = 0.05f;
    public float movementSpeed;
    [SerializeField] ContactFilter2D attackFilter; //For raycast layers
    [SerializeField] ContactFilter2D movementFilter; //For raycast layers
    List<RaycastHit2D> castedCollisions = new List<RaycastHit2D>();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rb.velocity = movementInput * movementSpeed;

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
