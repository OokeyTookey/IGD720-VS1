using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float Health
    {
        set
        {
            health = value;

            if (health <= 0)
            {
                Death();
            }
        }
        get
        {
            return health;
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public void Death()
    {
        Debug.Log("Enemy died");
    }

}
