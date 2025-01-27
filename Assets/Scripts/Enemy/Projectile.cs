using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D projectileRigidBody;
    [SerializeField] private BoxCollider2D projectileCollider;

    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    [SerializeField] private int damage = 1;
    protected Transform playerTransform;

    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;

        if (playerTransform.position.x > transform.position.x)
        {
            speed = -speed;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }

        projectileRigidBody.velocity = new Vector2(-speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }

        if (!collision.CompareTag("Enemy"))
        {
            Destroy(gameObject); 
        }
    }
}
