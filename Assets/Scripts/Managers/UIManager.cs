using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Wow this is disgusting but I have no time to fix this :(
    [Header("Health Settings")]
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private RectTransform healthContainer;

    [Header("Score Settings")]
    [SerializeField] private TMP_Text scoreText;

    [Header("PauseMenu")]
    public static CanvasGroup pauseMenu;
    private GameManager gameManager;

    private List<GameObject> hearts = new List<GameObject>();

    private void Awake()
    {
        pauseMenu = GetComponentInChildren<CanvasGroup>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Start()
    {
        scoreText.SetText("Enemies Killed: " + 0);
    }

    public void Update()
    {
        if (EnemySpawner.enemiesKilled < 11) // Sob emoji
        {
            scoreText.SetText("Enemies Killed: " + EnemySpawner.enemiesKilled); 
        }
        else
        {
            scoreText.SetText("Enemies Killed: " + EnemySpawner.enemiesKilled + "Get to the Skibidi!");
        }
    }

    public void UpdateHealth(int playerHealth)
    {
        // Add hearts at the start
        while (hearts.Count < playerHealth)
        {
            GameObject heart = Instantiate(heartPrefab, healthContainer);
            hearts.Add(heart);
        }

        // Remove hearts if there are too many
        while (hearts.Count > playerHealth)
        {
            GameObject heartToRemove = hearts[hearts.Count - 1];
            hearts.RemoveAt(hearts.Count - 1);
            Destroy(heartToRemove);
            healthContainer.sizeDelta = new Vector2(healthContainer.sizeDelta.x - 55, healthContainer.sizeDelta.y);
            healthContainer.localPosition = new Vector2(healthContainer.localPosition.x + 27.5f, healthContainer.localPosition.y);
        }
    }
    public static void Pause()
    {
        pauseMenu.alpha = 1.0f;
        pauseMenu.interactable = true;
        pauseMenu.blocksRaycasts = true;
        Time.timeScale = 0f;
    }

    public static void Unpause()
    {
        pauseMenu.alpha = 0.0f;
        pauseMenu.interactable = false;
        pauseMenu.blocksRaycasts = false;
        Time.timeScale = 1f;
    }

    public void MenuButton()
    {
        gameManager.LoadScene("MainMenu", gameObject.scene.name);
        Time.timeScale = 1f;
    }
}
