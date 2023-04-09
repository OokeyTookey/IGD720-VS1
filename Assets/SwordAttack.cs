using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    PlayerController playerController;
    Collider2D swordCollider;
    public Vector2 rightAttackOffset;
    public Vector2 upAttackOffset;
    public Vector2 downAttackOffset;

    Vector2 originalPosition;

    private void Start()
    {
        originalPosition= transform.position;
        playerController = FindObjectOfType<PlayerController>();
        swordCollider = GetComponent<Collider2D>();
    }

    public void SwordAttackRight()
    {
        transform.localPosition = originalPosition;
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }
     
    public void SwordAttackLeft()
    {
        transform.localPosition = originalPosition;
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x *-0.5f, rightAttackOffset.y);
    }

    public void SwordAttackUp()
    {
        transform.localPosition = originalPosition;
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(upAttackOffset.x, upAttackOffset.y);
    }

    public void SwordAttackDown()
    {
        transform.localPosition = originalPosition;
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(downAttackOffset.x, downAttackOffset.y);
    }


    public void StopAttack()
    {
        swordCollider.enabled = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy enemy= collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.health -= playerController.swordDamage;
            }
        }
    }
}
