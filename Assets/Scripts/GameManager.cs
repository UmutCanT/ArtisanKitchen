using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance { get; private set; }

    public event EventHandler OnGameStateChanged;

    private GameStates gameState;
    private float waitingToCountdown = 1f;
    private float countdownToStart = 3f;
    public float CountdownToStart => countdownToStart;

    private void Awake()
    {
        Instance = this;
        gameState = GameStates.WaitingToCountdown;
    }

    private void Start()
    {
        
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
                    gameState = GameStates.GameLoop;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameStates.GameLoop:
                break;
            case GameStates.GameOver:
                break;
            default:
                break;
        }
        Debug.Log(gameState.ToString());
    }

    public bool IsGamePlaying() => gameState == GameStates.GameLoop;

    public bool IsCountdown() => gameState == GameStates.Countdown;

    public enum GameStates
    {
        WaitingToCountdown,
        Countdown,
        GameLoop,
        GameOver
    }
}
