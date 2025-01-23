using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int enemyHealth;
    Vector2 impulse = new Vector2(-7, 3); // skopiro sa stack overflow ali brate moj taj x ne radi nista

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            collision.GetComponent<Rigidbody2D>().AddForce(impulse, ForceMode2D.Impulse);
        }
    }

    public void TakeDamage(int damageTaken)
    {
        enemyHealth -= damageTaken;
        if (enemyHealth < 0)
        {
            // Umri
        }
        else
        {
            // Animacija za hit onak
        }
    }
}
