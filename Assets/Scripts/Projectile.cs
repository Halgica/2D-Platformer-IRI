using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float lifetime;
    [SerializeField] private float resetTime;
    private float direction;
    private bool hit;
    [SerializeField] private int damage;

    private BoxCollider2D boxCollider;
    private Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        lifetime = 0;
        hit = false;
        gameObject.SetActive(true);
        //boxCollider.enabled = true;
    }

    private void Update()
    {
        if (hit) return;

        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }

        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

    }

    // public void setDirection(float _direction)
    // {
    //     direction = _direction;
    //     gameObject.SetActive(true);
    //     hit = false;
    //     boxCollider.enabled = true;

    //     float localScaleX = transform.localScale.x;
    //     if (Mathf.Sign(localScaleX) != _direction)
    //         localScaleX = -localScaleX;

    //     transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);   
    // }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}