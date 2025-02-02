using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Singleton? ja briem da se tak pise a rjesava se scriptable objektima
    //public static GameManager Instance;

    //private AlienController alienController;
    public string[] sceneNames;
    public int counter;
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private GameObject endPointPrefab;
    private bool hasSpawnedEndpoint = false;

    private void Awake()
    {
        //alienController = controller;
        //if (Instance != null)
        //    Destroy(Instance.gameObject);

        //Instance = this;
        LoadScene("MainMenu");
    }

    private void Update()
    {
        if (EnemySpawner.enemiesKilled == 10 && !hasSpawnedEndpoint)
        {
            SpawnEndpoint();
            hasSpawnedEndpoint=true;
        }
    }

    public void LoadScene(string sceneName, string unloadSceneName = null)
    {
        if (unloadSceneName != null)
            SceneManager.UnloadSceneAsync(unloadSceneName);

        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    private void SpawnEndpoint()
    {
        Instantiate(endPointPrefab, new Vector2(6f, -1.5f), Quaternion.identity);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
