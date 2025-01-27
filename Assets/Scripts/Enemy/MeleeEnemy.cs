using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private BoxCollider2D enemyCollider;
    [SerializeField] private Rigidbody2D enemyRigidBody;

    [SerializeField] private float range;
    [SerializeField] private float speed;
    [SerializeField] private float stoppingDistance;
    private bool direction;
    private bool movingRight = true;
    private bool isAttacking = false;

    private void Update()
    {
        if (!isDead)
        {
            FollowPlayer();
            DetectPlayer(); 
        }
        else
        {
            enemyRigidBody.bodyType = RigidbodyType2D.Static;
            enemyCollider.enabled = false;
        }
    }

    private void FollowPlayer()
    {
        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            // Check the distance to stop moving when close enough
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer > stoppingDistance)
            {
                enemyRigidBody.velocity = new Vector2(direction.x * speed, enemyRigidBody.velocity.y);
                enemyAnimator.SetBool("IsMoving", true);
            }
            else
            {
                enemyRigidBody.velocity = new Vector2(0, enemyRigidBody.velocity.y);
                enemyAnimator.SetBool("IsMoving", false);
            }

            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                movingRight = true;
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
                movingRight = false;
            }
        }
    }

    private void DetectPlayer()
    {
        // Raycast needs to change direction
        RaycastHit2D hit = Physics2D.Raycast(transform.position, SetDirection(), enemyCollider.bounds.extents.x + range, playerLayer);

        if (hit.collider != null && !isAttacking)
        {
            isAttacking = true;
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
        isAttacking = false;
    }

    private Vector3 SetDirection()
    {
        direction = movingRight;
        if (direction)
        {
            return transform.right;
        }
        else
            return -transform.right;
    }

}
