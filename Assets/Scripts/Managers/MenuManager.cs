using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    public void StartButton()
    {
        gameManager.LoadScene("Game", "MainMenu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
