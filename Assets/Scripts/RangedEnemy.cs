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
    [SerializeField] private GameObject[] projectiles;

    [Header("Collider parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    //references
    private Animator anim;
    // MeleeEnemy is the patrol script im just restarted
    private MeleeEnemy enemyPatrol;

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<MeleeEnemy>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown && playerInSight())
        {
            cooldownTimer = 0;
            anim.SetTrigger("rangedAttack");
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !playerInSight();
        }
    }

    private void rangedAttack()
    {
        cooldownTimer = 0;
        projectiles[findFireball()].transform.position = firepoint.position;
        projectiles[findFireball()].GetComponent<Projectile>().activateProjectile();
    }

    private int findFireball()
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private bool playerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
        , 0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}