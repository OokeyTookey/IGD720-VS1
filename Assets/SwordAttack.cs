using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    PlayerController playerController;
    public Collider2D swordCollider;
    public Vector2 rightAttackOffset;
    public Vector2 upAttackOffset;
    public Vector2 downAttackOffset;
    public Animator playerAnim;

    public Enemy currentEnemyHit;

    public bool enableAttack = false;

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
                currentEnemyHit.ReduceHp(playerController.swordDamage);
                enableAttack = false;
            }
        }
    }
}