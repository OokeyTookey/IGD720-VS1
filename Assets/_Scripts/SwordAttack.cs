using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    PlayerController playerController;
    public Collider2D swordCollider;
    public Animator playerAnim;

    public Enemy currentEnemyHit;
    public Rigidbody2D currentEnemyRB;

    public bool enableAttack = false;
    public float knockbackForce;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void UpdateBoxColliderPosition(Vector2 newPosition)
    {
        transform.localPosition = newPosition;
    }

    void OnTriggerStay2D(Collider2D other) // collision issues need to solve. Change this to on collision enter?
    {
        if (other.tag == "Enemy")
        {
            if (enableAttack)
            {
                currentEnemyHit = other.gameObject.GetComponent<Enemy>();
                currentEnemyRB = other.gameObject.GetComponent<Rigidbody2D>();

                if (currentEnemyHit.health > 0)
                {
                    currentEnemyHit.ReduceHp(playerController.swordDamage);

                    currentEnemyHit.knockedBack = true;

                     Vector2 direction = (currentEnemyHit.transform.position - playerController.transform.position).normalized;
                     currentEnemyRB.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
                    currentEnemyHit.knockBackTimer = 0;
                    enableAttack = false;
                }
                else
                {
                    currentEnemyRB.velocity= Vector2.zero;
                }

               
            }
        }
    }


}