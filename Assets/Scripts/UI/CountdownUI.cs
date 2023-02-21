using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CountdownUI : MonoBehaviour
{
    public static event EventHandler OnCountdownNumberChange;

    private const string COUNTDOWN_ANIM_TRIGGER = "PopUp";

    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI countdownText;

    private int previousCountdownNumber;
    private int countdownNumber;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        UIActive(false);
    }

    private void Update()
    {
        countdownNumber = Mathf.CeilToInt(GameManager.Instance.CountdownToStart);
        countdownText.text = countdownNumber.ToString();

        if (previousCountdownNumber != countdownNumber)
        {
            previousCountdownNumber = countdownNumber;
            animator.SetTrigger(COUNTDOWN_ANIM_TRIGGER);
            OnCountdownNumberChange?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameManager_OnGameStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdown())
        {
            UIActive(true);
        }
        else
        {
            UIActive(false);
        }
    } 

    public void UIActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
