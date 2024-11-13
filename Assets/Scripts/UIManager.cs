using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{


    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        scoreText.SetText("Score: " + 0);
    }

    public void updateScore(int playerScore)
    {
        scoreText.SetText("Score: " + playerScore.ToString());
    }
}
