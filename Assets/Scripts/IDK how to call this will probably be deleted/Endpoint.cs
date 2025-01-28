using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endpoint : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private string currentSceneName;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    public void LoadNextScene()
    {
        gameManager.LoadScene(nextSceneName,currentSceneName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LoadNextScene();
        EnemySpawner.enemiesKilled = 0;
        Destroy(gameObject);
    }
}
