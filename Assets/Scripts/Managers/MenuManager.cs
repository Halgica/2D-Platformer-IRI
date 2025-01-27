using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private GameManager gameManager;
    private CanvasGroup menuCanvasGroup;
    private CanvasGroup optionsCanvasGroup;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        // Maybe make this pretty if there is time
        menuCanvasGroup = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<CanvasGroup>();
        optionsCanvasGroup = GameObject.FindGameObjectWithTag("OptionMenu").GetComponent<CanvasGroup>();
    }

    public void StartButton()
    {
        gameManager.LoadScene("Game", "MainMenu");
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
}
