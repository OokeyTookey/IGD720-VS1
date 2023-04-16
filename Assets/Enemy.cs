using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyDetectionRange;
    public float movementSpeed;
    
    bool awareOfPlayer;
    Vector2 directionToPlayer;
    Vector2 targetDirection;

    private Vector2 dampMovement;
    private Vector2 dampMovementVelocity;
    public float turnSpeed;

    PlayerController player;
    Rigidbody2D rb;

    float dizzyTimer;
    public float dizzyTimerWait;

    bool enemyHit;
    public float knockbackForce;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        dizzyTimer += Time.deltaTime;

        Vector2 distanceFromEnemyToPlayer = player.transform.position - transform.position;
        directionToPlayer = distanceFromEnemyToPlayer.normalized;

        if (distanceFromEnemyToPlayer.magnitude <= enemyDetectionRange)
        {
            awareOfPlayer = true;
        }
        else
        {
            awareOfPlayer = false;
        }
    }

    private void FixedUpdate()
    {
        if (enemyHit) //Enemy has to wait x seconfs before they go
        {
            dizzyTimer = 0;
            enemyHit = false;
        }

        if (dizzyTimer >= dizzyTimerWait)
        {
        UpdateTargetDirection();
        SetEnemyVelocity();
        }
    }

    void UpdateTargetDirection()
    {
        if (awareOfPlayer)
        {
            Debug.Log("AAA");
            targetDirection = directionToPlayer;
        }
        else
        {
            targetDirection = Vector2.zero;
        }
    }

    void SetEnemyVelocity()
    {
        if (targetDirection == Vector2.zero)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            dampMovement = Vector2.SmoothDamp(dampMovement,targetDirection, ref dampMovementVelocity,turnSpeed);
            rb.velocity = dampMovement * movementSpeed;
        }          
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyDetectionRange);
    }


    [SerializeField] int health;

    public void ReduceHp(int value)
    {
        Animator animations = this.GetComponent<Animator>();
        health -= value;
        animations.SetTrigger("EnemyHit");

        EnemyKnockback();

        if (health <= 0)
        {
            animations.SetTrigger("EnemyDie");
        }
    }

    void EnemyKnockback()
    {
        Debug.Log("knockback");
        enemyHit = true;
        Vector2 direction = (transform.position - player.transform.position).normalized;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }
}