using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            Debug.Log("LoOSING HALTH");
            Death();
        }
    }
    

    public void Death()
    {
        Animator animations = this.GetComponent<Animator>();
        animations.SetTrigger("EnemyDie");
        Debug.Log("Enemy died");
    }

}
