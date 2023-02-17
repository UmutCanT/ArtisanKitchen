using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        UIActive(false);
    }

    private void Update()
    {
        countdownText.text = Mathf.Ceil(GameManager.Instance.CountdownToStart).ToString();
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
