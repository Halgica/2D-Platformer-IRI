using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PatrolScript : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject leftEdge;
    [SerializeField] private GameObject rightEdge;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float pauseDuration = 1f;

    //private List<Transform> patrolPoints; mejbi bolji nacin :3
    //private Transform currentPatrolPoint;
    //private Transform nextPatrolPoint;
    //i ond ides po indeksima tih patrol pointa (vector3.Lerp)

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator enemyAnimator;
    public bool movingRight = true;
    private bool isPaused = false;
    public bool isAttacking = false;

    void Awake()
    {
        rb = enemy.GetComponent<Rigidbody2D>();
        sr = enemy.GetComponent<SpriteRenderer>();
        enemyAnimator = enemy.GetComponent<Animator>();
    }

    void Update()
    {
        if (!isPaused && !isAttacking)
        {
            Move();
        }
    }

    private void Move()
    {
        enemyAnimator.SetBool("IsMoving", true);
        if (movingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);

            if (enemy.transform.position.x >= rightEdge.transform.position.x)
            {
                StartCoroutine(PauseMovement());
            }
        }
        
        else
        {
            rb.velocity = new Vector2 (-speed, rb.velocity.y);

            if (enemy.transform.position.x <= leftEdge.transform.position.x)
            {
                StartCoroutine(PauseMovement());
            }
        }
    }

    public IEnumerator PauseMovement()
    {
        enemyAnimator.SetBool("IsMoving", false);
        isPaused = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(pauseDuration);
        movingRight = !movingRight;
        sr.flipX = !movingRight;
        isPaused = false;
    }
}
