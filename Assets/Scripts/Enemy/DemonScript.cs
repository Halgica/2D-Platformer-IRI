using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonScript : Enemy
{
    [SerializeField] private Rigidbody2D enemyRigidBody;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private float range;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float attackCooldown;

    void Update()
    {
        if (!isDead)
        {
            FollowPlayer();

            if (attackCooldown > 0)
            {
                attackCooldown -= Time.deltaTime;
            } 
        }
        else
        {
            enemyRigidBody.bodyType = RigidbodyType2D.Static;
        }
    }


    private void FollowPlayer()
    {
        if (playerTransform != null)
        {
            // Calculate the direction towards the player, considering both x and y positions
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            // Check the distance to stop moving when close enough horizontally
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            // Move towards the player if we're outside the stopping distance horizontally
            if (distanceToPlayer > stoppingDistance)
            {
                // Move the enemy both horizontally and vertically with different speeds
                enemyRigidBody.velocity = new Vector2(direction.x * horizontalSpeed, direction.y * verticalSpeed);
            }
            else
            {
                // Move the enemy vertically to match the player's Y position, while stopping horizontally
                enemyRigidBody.velocity = new Vector2(0, direction.y * verticalSpeed);

                // If attack cooldown is finished, perform attack
                if (attackCooldown <= 0)
                {
                    enemyAnimator.SetTrigger("Attack");
                    attackCooldown = 2f; // Reset attack cooldown
                }
            }

            // Flip the enemy based on the player's horizontal position (left or right)
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Face right
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1); // Face left
            }
        }
    }




    public void FireProjectile()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation, firePoint);
    }
}
