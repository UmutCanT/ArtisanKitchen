using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance { get; private set; }

    private GameStates gameState;
    private float waitingToCountdown = 1f;
    private float countdownToStart = 3f;

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
                }
                break;
            case GameStates.Countdown:
                countdownToStart -= Time.deltaTime;
                if (countdownToStart < 0)
                {
                    gameState = GameStates.GameLoop;
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

    public enum GameStates
    {
        WaitingToCountdown,
        Countdown,
        GameLoop,
        GameOver
    }
}
