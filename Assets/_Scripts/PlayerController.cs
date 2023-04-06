using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movementInput;


    public float movementSpeed;
    public float collsionOffset = 0.05f;
    public ContactFilter2D movementFilter; //For raycast layers
    List<RaycastHit2D> castedCollisions = new List<RaycastHit2D>();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero) 
        {
           int count = rb.Cast(movementInput, movementFilter, castedCollisions, movementSpeed * Time.fixedDeltaTime + collsionOffset);

            if (count == 0)
            {
                rb.MovePosition(rb.position + movementInput * movementSpeed * Time.fixedDeltaTime);
            }
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
