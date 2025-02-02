using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private BoxCollider2D playerBoxCollider;
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject swordSwingSFX;
    [SerializeField] private Transform AudioContainer;

    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float attackCooldown;
    private float attackTimer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && attackTimer <= 0f)
        {
            Attack();
        }
        attackTimer -= Time.deltaTime;
    }

    private void Attack()
    {
        playerAnimator.SetTrigger("Attack");
        Instantiate(swordSwingSFX, AudioContainer.position, Quaternion.identity, transform);
        Collider2D[] Enemies = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (var hitCollider in Enemies)
        {
            if (hitCollider.TryGetComponent<Enemy>(out Enemy e))
            {
                e.TakeDamage(1);
            }
        }
        attackTimer = attackCooldown;
    }
}
