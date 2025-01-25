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
    private int score = 0;
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private LayerMask deathLayer; 
    private AlienState curState;
    private int STATE_HASH = Animator.StringToHash("State");

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

        if (playerRigidBody.velocity.x != 0 && isGrounded)
        {
            curState = AlienState.Walk;
        }

        else if (!isGrounded && playerRigidBody.velocity.y > 0)
        {
            curState = AlienState.Jump;
        }

        else if (!isGrounded && playerRigidBody.velocity.y < 0)
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

