using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;

    private void Start()
    {
        mainMenuButton.onClick.AddListener(() => {
            Time.timeScale = 1f;
            SceneLoader.Load(SceneLoader.Scene.MainMenuScene);
        });

        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.TogglePauseGame();
        });

        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        UIActive(false);
    }

    private void OnEnable()
    {
        resumeButton.Select();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        UIActive(false);
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        UIActive(true);
    }

    public void UIActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
