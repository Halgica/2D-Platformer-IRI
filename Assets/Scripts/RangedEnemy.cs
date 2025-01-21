using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Player layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer;

    [Header("Attack parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float range;

    [Header("Ranged attack")]
    [SerializeField] private Transform firepoint;
    private Vector3 projectileFirePoint;
    [SerializeField] private GameObject[] projectiles;

    [Header("Collider parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    private bool direction;

    
    private Animator anim;
    // MeleeEnemy is the patrol script im just restarted
    private PatrolScript enemyPatrol;

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<PatrolScript>();
        direction = enemyPatrol.movingRight;
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown && PlayerInSight())
        {
            cooldownTimer = 0;
            //anim.SetTrigger("rangedAttack");
            RangedAttack();
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private void RangedAttack()
    {
        // imma kms bro
        if (direction)
            projectileFirePoint = firepoint.position + new Vector3(0.7f, 0);
        else
            projectileFirePoint = firepoint.position + new Vector3(-0.7f, 0);

        cooldownTimer = 0;
        // Find inactive projectile in array and shoot it
        projectiles[FindFireball()].transform.position = firepoint.position;
        projectiles[FindFireball()].GetComponent<Projectile>().ActivateProjectile();
    }

    private int FindFireball()
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + SetDirection() * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
        , 0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + SetDirection() * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
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