using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] counterVisualsArray;

    private void OnEnable()
    {
        FindObjectOfType<Player>().GetComponent<Player>().OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            ShowCounter(true);
        }
        else
        {
            ShowCounter(false);
        }
    }

    private void ShowCounter(bool isActive)
    {
        foreach (GameObject visualGameObject in counterVisualsArray)
        {
            visualGameObject.SetActive(isActive);
        }
    }
}
