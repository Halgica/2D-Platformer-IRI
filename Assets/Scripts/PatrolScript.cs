using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class MeleeEnemy : MonoBehaviour
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
    private bool movingRight = true;
    private bool isPaused = false;

    void Awake()
    {
        rb = enemy.GetComponent<Rigidbody2D>();
        sr = enemy.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isPaused)
        {
            Move();
            Flip();
        }
    }

    private void Move()
    {
        if (movingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);

            if (enemy.transform.position.x >= rightEdge.transform.position.x)
            {
                StartCoroutine(PauseAtEdge());
                movingRight = false;
            }
        }
        
        else
        {
            rb.velocity = new Vector2 (-speed, rb.velocity.y);

            if (enemy.transform.position.x <= leftEdge.transform.position.x)
            {
                StartCoroutine(PauseAtEdge());
                movingRight = true;
            }
        }
    }

    private void Flip()
    {
        sr.flipX = movingRight;
    }

    private IEnumerator PauseAtEdge()
    {
        isPaused = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(pauseDuration);
        isPaused = false;
    }
}
