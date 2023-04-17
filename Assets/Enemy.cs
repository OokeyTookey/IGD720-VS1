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

    Rigidbody2D rb;

    float dizzyTimer;
    public float dizzyTimerWait;

    bool enemyHit;
    private float changeDirectionCooldown;
    public int health;

    PlayerController player;
    Rigidbody2D playerRB;

    SpriteRenderer enemyImage;

    bool enemyDead;

    private void Awake()
    {
        targetDirection = transform.up;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        rb = this.GetComponent<Rigidbody2D>();
        playerRB = player.GetComponent<Rigidbody2D>();
        enemyImage = this.GetComponent<SpriteRenderer>();
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
            if (!enemyDead)
            {
                UpdateTargetDirection();
                SetEnemyVelocity();
            }
        }
    }

    void UpdateTargetDirection()
    { 
        RandomTargetDirection();
        PlayerTargeting();
    }

    void PlayerTargeting()
    {
        if (awareOfPlayer)
        {
            targetDirection = directionToPlayer;
        }
    }

    void RandomTargetDirection()
    {
        changeDirectionCooldown -= Time.deltaTime;
        if (changeDirectionCooldown <=0)
        {
            float angleChange = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            
            targetDirection =  rotation * targetDirection;
            changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }

    void SetEnemyVelocity()
    {
        if (!enemyDead)
        {
            dampMovement = Vector2.SmoothDamp(dampMovement, targetDirection, ref dampMovementVelocity, turnSpeed);
            rb.velocity = dampMovement * movementSpeed;
        }   
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyDetectionRange);
    }

    public void ReduceHp(int value)
    {
        Animator animations = this.GetComponent<Animator>();
        health -= value;
        animations.SetTrigger("EnemyHit");

        if (health <= 0)
        {
            animations.SetTrigger("EnemyDie");
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            enemyDead = true;
            StartCoroutine(FadeOut(8f, enemyImage));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody.tag == "Player")
        {
            player.playerHealth--;
            if (player.playerHealth <= 0)
            {
                player.PlayerDeath();
            }
        }
    }

    public IEnumerator FadeOut(float time, SpriteRenderer image)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        while (image.color.a > 0.0f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (Time.deltaTime / time));
            yield return null;
        }
    }
}