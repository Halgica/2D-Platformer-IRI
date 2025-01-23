using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AlienController : MonoBehaviour
{
    public enum AlienState { Idle, Walk, Jump, Fall };

    [SerializeField] private Checkpoint currentCheckPoint;
    [SerializeField] private BoxCollider2D playerBoxCollider;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private UIManager Manager;
    private GameManager gameManager;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private int score = 0;
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private LayerMask deathLayer; 
    private AlienState curState;
    private int STATE_HASH = Animator.StringToHash("State");

    private Rigidbody2D rb;

    [SerializeField] private GameObject sfxAlienDeath;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Manager = FindFirstObjectByType<UIManager>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Update()
    {
        if(IsColliding(deathLayer))
        {
            transform.position = currentCheckPoint.GetSpawnPoint().position;
            Instantiate(sfxAlienDeath, transform.position, Quaternion.identity);
        }

        // Movement
        float moveInput = Input.GetAxis("Horizontal");

        if (moveInput != 0)
        {
            spriteRenderer.flipX = moveInput < 0;
        }

        bool isGrounded = IsColliding(groundLayer);


        float groundSpeed = isGrounded ? Mathf.Abs(moveInput * moveSpeed) : 0;
        

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (rb.velocity.x != 0 && isGrounded)
        {
            curState = AlienState.Walk;
        }

        else if (!isGrounded && rb.velocity.y > 0)
        {
            curState = AlienState.Jump;
        }

        else if (!isGrounded && rb.velocity.y < 0)
        {
            curState = AlienState.Fall;
        }

        else
        {
            curState = AlienState.Idle;
        }

        animator.SetInteger(STATE_HASH, (int)curState);
    }

    private bool IsColliding(LayerMask layerMask)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, playerBoxCollider.bounds.extents.y + 0.1f, layerMask);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        // Visualize the ground detection ray in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (playerBoxCollider.bounds.extents.y + 0.1f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Coin":
                Destroy(collision.gameObject);
                addScore();
                break;

            //case "Checkpoint":
            //    currentCheckPoint?.SetIndicatorColor(false);
            //    currentCheckPoint = collision.GetComponent<Checkpoint>();
            //    currentCheckPoint.SetIndicatorColor(true);
            //    break;

            //case "Endpoint":
            //    collision.GetComponent<Endpoint>().LoadNextScene();
            //    break;
        }
    }
    public void addScore()
    {
        score++;
        Manager.UpdateScore(score);
    }

    public void DisableMovement()
    {
        this.enabled = false;
    }
}

