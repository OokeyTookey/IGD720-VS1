using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    PlayerController playerController;
    public Collider2D swordCollider;
    public Animator playerAnim;
  //  public Vector2 rightAttackOffset;
  //  public Vector2 upAttackOffset;
  //  public Vector2 downAttackOffset;

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

     void OnTriggerStay2D(Collider2D other)
     {
         if (other.tag == "Enemy")
         {
             if (enableAttack)
             {
                 currentEnemyHit = other.gameObject.GetComponent<Enemy>();
                 currentEnemyRB = other.gameObject.GetComponent<Rigidbody2D>();

                 currentEnemyHit.ReduceHp(playerController.swordDamage);

                 Vector2 direction = (currentEnemyHit.transform.position - playerController.transform.position);
                 currentEnemyRB.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
                 enableAttack = false;
             }
         }
     }
}