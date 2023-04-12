using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    public void Death()
    {
        Animator animations = this.GetComponent<Animator>();
        animations.SetTrigger("EnemyDie");
        Debug.Log("Enemy died");
    }

}
