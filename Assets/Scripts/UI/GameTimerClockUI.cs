using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerClockUI : MonoBehaviour
{
    [SerializeField] private Image clockImage;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        UIActive(true);
    }

    private void Update()
    {
        clockImage.fillAmount = GameManager.Instance.LeftTimeAmountNormalizedInverted();
    }

    private void GameManager_OnGameStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
            UIActive(false);
    }

    public void UIActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
