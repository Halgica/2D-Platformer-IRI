using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
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
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private LayerMask deathLayer; 
    private AlienState curState;
    private int STATE_HASH = Animator.StringToHash("State");
    private bool isPaused = false;

    private Rigidbody2D playerRigidBody;

    [SerializeField] private GameObject sfxAlienDeath;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
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

        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0) // why did i have to do this u ask? cuz of a unity moment
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        bool isGrounded = IsColliding(groundLayer);


        float groundSpeed = isGrounded ? Mathf.Abs(moveInput * moveSpeed) : 0;
        

        playerRigidBody.velocity = new Vector2(moveInput * moveSpeed, playerRigidBody.velocity.y);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            UIManager.Pause();
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            UIManager.Unpause();
            isPaused = false;
        }

        // State management
        if (isGrounded)
        {
            if (playerRigidBody.velocity.x != 0)
            {
                curState = AlienState.Walk;  // Walk when moving horizontally
            }
            else
            {
                curState = AlienState.Idle;  // Idle when not moving horizontally
            }
        }
        else
        {
            // If player is in the air (not grounded)
            if (playerRigidBody.velocity.y > 0)
            {
                curState = AlienState.Jump;  // Jump state for upward movement
            }
            else if (playerRigidBody.velocity.y < 0)
            {
                curState = AlienState.Fall;  // Fall state for downward movement
            }
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

    public void DisableMovement()
    {
        this.enabled = false;
    }
}

