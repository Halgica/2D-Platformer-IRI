using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Sprite[] healthImages;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Image healthRenderer;

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
        healthRenderer.sprite = healthImages[playerHealth];
    }
}
