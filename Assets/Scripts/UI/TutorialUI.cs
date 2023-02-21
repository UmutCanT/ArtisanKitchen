using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{


    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        UIActive(true);
    }

    private void GameManager_OnGameStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdown())
        {
            UIActive(false);
            GameManager.Instance.OnGameStateChanged -= GameManager_OnGameStateChanged;
        }
    }

    public void UIActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
