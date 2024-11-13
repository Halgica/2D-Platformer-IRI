using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AlienController : MonoBehaviour
{
    [SerializeField] private Checkpoint currentCheckPoint;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private GameObject UI;
    private UIManager Manager;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private int score = 0;
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private LayerMask deathLayer; 

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Manager = UI.GetComponent<UIManager>();
    }

    private void Update()
    {
        if(IsColliding(deathLayer))
        {
            transform.position = currentCheckPoint.GetSpawnPoint().position; 
        }

        // Movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && IsColliding(groundLayer))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private bool IsColliding(LayerMask layerMask)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, boxCollider.bounds.extents.y + 0.1f, layerMask);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        // Visualize the ground detection ray in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (boxCollider.bounds.extents.y + 0.1f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Coin":
                Destroy(collision.gameObject);
                addScore();
                break;

            case "Checkpoint":
                currentCheckPoint?.SetIndicatorColor(false);
                currentCheckPoint = collision.GetComponent<Checkpoint>();
                currentCheckPoint.SetIndicatorColor(true);
                break;
        }
    }
    public void addScore()
    {
        score++;
        Manager.updateScore(score);
    }
}

