using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Singleton? ja briem da se tak pise a rjesava se scriptable objektima
    //public static GameManager Instance;

    //private AlienController alienController;

    private void Start()
    {
        //alienController = controller;
        //if (Instance != null)
        //    Destroy(Instance.gameObject);

        //Instance = this;
        LoadScene("Game");
    }

    public void LoadScene(string sceneName, string unloadSceneName = null)
    {
        if (unloadSceneName != null)
            SceneManager.UnloadSceneAsync(unloadSceneName);

        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
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
