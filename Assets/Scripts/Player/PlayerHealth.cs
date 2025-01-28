using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private PlayerCombat playerCombat;
    private UIManager uiManager;
    private GameManager gameManager;

    private void Awake()
    {
        health = maxHealth;
        gameManager = FindAnyObjectByType<GameManager>();
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
            playerCombat.enabled = false;
            playerRB.bodyType = RigidbodyType2D.Static;
            StartCoroutine(LoadDeathScreen());
        }
        else
        {
            playerAnimator.SetTrigger("Damage");
        }
    }

    public IEnumerator LoadDeathScreen()
    {
        Debug.Log("Tu sam");
        yield return new WaitForSeconds(2);
        gameManager.LoadScene("DeathScreen", playerAnimator.gameObject.scene.name);
    }
    
}
