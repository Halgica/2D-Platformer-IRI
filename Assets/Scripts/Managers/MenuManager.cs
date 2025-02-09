using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private GameManager gameManager;
    private CanvasGroup menuCanvasGroup;
    private CanvasGroup optionsCanvasGroup;
    private CanvasGroup levelCanvasGroup;
    [SerializeField] private Button Level2;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        // Maybe make this pretty if there is time
        menuCanvasGroup = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<CanvasGroup>();
        optionsCanvasGroup = GameObject.FindGameObjectWithTag("OptionMenu").GetComponent<CanvasGroup>();
        levelCanvasGroup = GameObject.FindGameObjectWithTag("LevelMenu").GetComponent<CanvasGroup>();
        Level2.enabled = false;
        Level2.GetComponent<Image>().enabled = false;
    }

    private void Update()
    {
        if (EnemySpawner.enemiesKilled >= 15f)
        {
            Level2.enabled = true;
            Level2.GetComponent<Image>().enabled = true;
        }
    }

    public void StartButton()
    {
        menuCanvasGroup.alpha = 0;
        menuCanvasGroup.interactable = false;
        menuCanvasGroup.blocksRaycasts = false;

        levelCanvasGroup.alpha = 1;
        levelCanvasGroup.interactable = true;
        levelCanvasGroup.blocksRaycasts = true;
    }

    public void QuitButton()
    {
        Application.Quit();
    }
    
    public void OptionsButton()
    {
        menuCanvasGroup.alpha = 0;
        menuCanvasGroup.interactable = false;
        menuCanvasGroup.blocksRaycasts = false;

        optionsCanvasGroup.alpha = 1;
        optionsCanvasGroup.interactable = true;
        optionsCanvasGroup.blocksRaycasts = true;
    }

    public void ExitOptionsButton()
    {
        optionsCanvasGroup.alpha = 0;
        optionsCanvasGroup.interactable = false;
        optionsCanvasGroup.blocksRaycasts = false;

        menuCanvasGroup.alpha = 1;
        menuCanvasGroup.interactable = true;
        menuCanvasGroup.blocksRaycasts = true;
    }

    public void LoadLevel1()
    {
        gameManager.LoadScene("Level1", "MainMenu");
    }
    public void LoadLevel2()
    {
        gameManager.LoadScene("Level2", "MainMenu");
    }
}
