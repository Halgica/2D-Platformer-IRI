using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private BoxCollider2D playerBoxCollider;
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        playerAnimator.SetTrigger("Attack");
        Collider2D[] Enemies = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (var hitCollider in Enemies)
        {
            if (hitCollider.TryGetComponent<MeleeEnemy>(out MeleeEnemy e))
            {
                e.TakeDamage(1);
            }
        }
    }
}
