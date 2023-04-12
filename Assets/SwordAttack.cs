using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    PlayerController playerController;
    public Collider2D swordCollider;
    public Vector2 rightAttackOffset;
    public Vector2 upAttackOffset;
    public Vector2 downAttackOffset;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        swordCollider = GetComponent<Collider2D>();
    }

    public void SwordAttackRight()
    {
        transform.localPosition = rightAttackOffset;
        swordCollider.enabled = true;
    }
     
    public void SwordAttackLeft()
    {
        transform.localPosition = new Vector3(rightAttackOffset.x *-0.5f, rightAttackOffset.y);
      swordCollider.enabled = true;
    }

    public void SwordAttackUp()
    {
        transform.localPosition = new Vector3(upAttackOffset.x, upAttackOffset.y);
      swordCollider.enabled = true;
    }

    public void SwordAttackDown()
    {
        transform.localPosition = new Vector3(downAttackOffset.x, downAttackOffset.y);
        swordCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("slime found");
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.health -= playerController.swordDamage;
            }
        }
    }
}
