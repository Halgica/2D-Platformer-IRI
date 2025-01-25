using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int enemyHealth;
    [SerializeField] protected int damage;

    [SerializeField] protected Animator enemyAnimator;

    public void TakeDamage(int damageTaken)
    {
        enemyHealth -= damageTaken;
        if (enemyHealth < 0)
        {
            enemyAnimator.SetTrigger("Death");
            Debug.Log("dead");
            Destroy(transform.parent.gameObject);
        }
        else
        {
            enemyAnimator.SetTrigger("Damaged");
        }
    }
}
