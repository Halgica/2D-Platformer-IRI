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
        gameManager.LoadScene(nextSceneName, unloadSceneName: currentSceneName);
    }
}
