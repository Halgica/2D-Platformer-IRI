using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject player;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void MenuButton()
    {
        gameManager.LoadScene("MainMenu", "DeathScreen");
    }
}
