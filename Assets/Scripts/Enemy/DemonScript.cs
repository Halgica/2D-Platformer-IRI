using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonScript : Enemy
{
    [SerializeField] private Rigidbody2D enemyRigidBody;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    private Transform projectileHolder;

    [SerializeField] private float range;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float attackCooldown;

    protected override void Awake()
    {
        base.Awake();
        projectileHolder = GameObject.FindWithTag("ProjectileHolder").transform;
    }

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
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer > stoppingDistance)
            {
                enemyRigidBody.velocity = new Vector2(direction.x * horizontalSpeed, direction.y * verticalSpeed);
            }
            else
            {
                enemyRigidBody.velocity = new Vector2(0, direction.y * verticalSpeed);

                if (attackCooldown <= 0)
                {
                    enemyAnimator.SetTrigger("Attack");
                    attackCooldown = 2f;
                }
            }

            if (direction.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void FireProjectile()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation, projectileHolder.transform);
    }
}
