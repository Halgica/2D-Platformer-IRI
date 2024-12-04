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

    private Rigidbody2D rb;
    private bool movingRight = true;
    private bool isPaused = false;

    void Awake()
    {
        rb = enemy.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isPaused)
        {
            Move();
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

    private IEnumerator PauseAtEdge()
    {
        isPaused = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(pauseDuration);
        isPaused = false;
    }
}
