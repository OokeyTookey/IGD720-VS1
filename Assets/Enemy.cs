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
        health -= value;
        print(value);
        if (health <= 0)
        {
            Animator animations = this.GetComponent<Animator>();
            animations.SetTrigger("EnemyDie");
        }
    }
}