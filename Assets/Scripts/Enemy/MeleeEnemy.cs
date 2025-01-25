using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    Vector2 impulse = new Vector2(-7, 3); // skopiro sa stack overflow ali brate moj taj x ne radi nista
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private BoxCollider2D enemyCollider;
    [SerializeField] private PatrolScript enemyPatrol;

    [SerializeField] private float range;
    private bool direction;

    private void Awake()
    {
        enemyPatrol = GetComponentInParent<PatrolScript>();
    }

    private void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        // Raycast needs to change direction
        RaycastHit2D hit = Physics2D.Raycast(transform.position, SetDirection(), enemyCollider.bounds.extents.x + range, playerLayer);

        if (hit.collider != null && !enemyPatrol.isAttacking)
        {
            enemyPatrol.isAttacking = true;
            enemyAnimator.SetTrigger("Attack");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + SetDirection() * (enemyCollider.bounds.extents.x + range));
    }

    public void DamagePlayer()
    {
        RaycastHit2D damageHit = Physics2D.Raycast(transform.position, SetDirection(), enemyCollider.bounds.extents.x + range, playerLayer);

        if (damageHit.collider != null)
        {
            damageHit.collider.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    public void AttackFinished()
    {
        enemyPatrol.isAttacking = false;
    }

    private Vector3 SetDirection()
    {
        direction = enemyPatrol.movingRight;
        if (direction)
        {
            return transform.right;
        }
        else
            return -transform.right;
    }

}
