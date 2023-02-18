using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const float TOTAL_GAME_TIME = 10f;
    public static GameManager Instance { get; private set; }

    public event EventHandler OnGameStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private GameStates gameState;
    private float waitingToCountdown = 1f;
    private float countdownToStart = 3f;
    private float timeLeftToGameOver;
    private bool isGamePaused = false;
    public float CountdownToStart => countdownToStart;

    private void Awake()
    {
        Instance = this;
        gameState = GameStates.WaitingToCountdown;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameStates.WaitingToCountdown:
                waitingToCountdown -= Time.deltaTime;
                if (waitingToCountdown < 0)
                {
                    gameState = GameStates.Countdown;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameStates.Countdown:
                countdownToStart -= Time.deltaTime;
                if (countdownToStart < 0)
                {
                    timeLeftToGameOver = TOTAL_GAME_TIME;
                    gameState = GameStates.GameLoop;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameStates.GameLoop:
                timeLeftToGameOver -= Time.deltaTime;
                if (timeLeftToGameOver < 0)
                {
                    gameState = GameStates.GameOver;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameStates.GameOver:
                break;
            default:
                break;
        }
    }

    public bool IsGamePlaying() => gameState == GameStates.GameLoop;

    public bool IsCountdown() => gameState == GameStates.Countdown;

    public bool IsGameOver() => gameState == GameStates.GameOver;

    public float LeftTimeAmountNormalizedInverted() => 1 - (timeLeftToGameOver / TOTAL_GAME_TIME);

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    public enum GameStates
    {
        WaitingToCountdown,
        Countdown,
        GameLoop,
        GameOver
    }
}
