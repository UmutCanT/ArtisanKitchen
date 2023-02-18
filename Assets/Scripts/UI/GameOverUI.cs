using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI orderCountText;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        UIActive(false);
    }

    private void GameManager_OnGameStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            UIActive(true);
            orderCountText.text = DeliveryManager.Instance.SuccessfulOrderCount.ToString();
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
