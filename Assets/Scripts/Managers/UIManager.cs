using System.Collections.Generic;
using TMPro;
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

    private List<GameObject> hearts = new List<GameObject>();

    private void Start()
    {
        scoreText.SetText("Score: " + 0);
    }

    public void UpdateScore(int playerScore)
    {
        scoreText.SetText("Score: " + playerScore.ToString());
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
}
