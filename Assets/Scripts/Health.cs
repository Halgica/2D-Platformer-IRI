using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private BoxCollider2D playerCollider;
    private UIManager uiManager;

    private void Awake()
    {
        health = maxHealth;
        uiManager = FindFirstObjectByType<UIManager>();
        uiManager.UpdateHealth(health);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        uiManager.UpdateHealth(health);

        if (health <= 0)
        {
            playerAnimator.SetTrigger("Death");
            playerCollider.enabled = false;
            playerRB.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            playerAnimator.SetTrigger("Damage");
        }
    }
    
}
