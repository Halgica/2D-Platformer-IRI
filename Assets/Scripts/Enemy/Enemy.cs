using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int enemyHealth;
    [SerializeField] protected int damage;
    protected bool isDead = false;

    [SerializeField] protected Animator enemyAnimator;
    protected Transform playerTransform;

    private void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    public void TakeDamage(int damageTaken)
    {
        enemyHealth -= damageTaken;
        if (enemyHealth < 0)
        {
            isDead = true;
            enemyAnimator.SetTrigger("Death");
            StartCoroutine(DestroyAfter2Seconds());
        }
        else
        {
            enemyAnimator.SetTrigger("Damaged");
        }
    }

    public IEnumerator DestroyAfter2Seconds()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
