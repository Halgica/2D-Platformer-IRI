using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private BoxCollider2D playerBoxCollider;
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    private void DealDamage()
    {
        RaycastHit2D hit = Physics2D.BoxCast(playerBoxCollider.bounds.center + SetDirection() * range * transform.localScale.x * colliderDistance,
       new Vector3(playerBoxCollider.bounds.size.x * range, playerBoxCollider.bounds.size.y, playerBoxCollider.bounds.size.z)
       , 0, Vector2.left, 0, enemyLayer);

        if (hit.collider != null)
        {
          // Hmm nema get component al treba sam dilat demaz tu  
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(playerBoxCollider.bounds.center + SetDirection() * range * transform.localScale.x * colliderDistance,
            new Vector3(playerBoxCollider.bounds.size.x * range, playerBoxCollider.bounds.size.y, playerBoxCollider.bounds.size.z));
    }

    private Vector3 SetDirection()
    {
        if (playerRenderer.flipX)
        {
            return transform.right;
        }
        else
            return -transform.right;
    }
}
