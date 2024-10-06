using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject levelSelectionUI;
    public Button playButton;
    public Button exitButton;
    public Button levelSelectionBackButton;

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonCicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
        levelSelectionBackButton.onClick.AddListener(OnLevelSelectionBackButtonClicked);
    }

    void OnPlayButtonCicked()
    {
        levelSelectionUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }

    void OnExitButtonClicked()
    {
        Application.Quit();
    }

    void OnLevelSelectionBackButtonClicked()
    {
        levelSelectionUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}
