using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health;

    /*private void FixedUpdate()
    {
        if (health <= 0)
        {
            Debug.Log("LoOSING HALTH");
            Death();
        }
    }*/

    public void ReduceHp(int value)
    {
        Animator animations = this.GetComponent<Animator>();
        health -= value;
        animations.SetTrigger("EnemyHit");
        if (health <= 0)
        {
            animations.SetTrigger("EnemyDie");
        }
    }
}