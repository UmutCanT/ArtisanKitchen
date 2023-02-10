using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualSelectedCounter;

    private void OnEnable()
    {
        FindObjectOfType<Player>().GetComponent<Player>().OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter)
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
        visualSelectedCounter.SetActive(isActive);
    }
}
