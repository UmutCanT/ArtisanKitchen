using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderResultUI : MonoBehaviour
{
    private const string POP_UP = "PopUp";

    [SerializeField] private Animator animator;
    [SerializeField] private Image background;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI messageText;

    [SerializeField] private Color onCorrectColor;
    [SerializeField] private Color onWrongColor;
    [SerializeField] private Sprite onCorrectSprite;
    [SerializeField] private Sprite onWrongSprite;
    private readonly string onCorrectText = "Correct\nOrder";
    private readonly string onWrongText = "Wrong\nOrder";


    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;
        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnDeliveryFailed(object sender, System.EventArgs e)
    {
        ChangeResultUI(onWrongColor, onWrongSprite, onWrongText);
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        ChangeResultUI(onCorrectColor, onCorrectSprite, onCorrectText);
    }

    private void ChangeResultUI(Color color, Sprite sprite, string text)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POP_UP);
        background.color = color;
        icon.sprite = sprite;
        messageText.text = text;
    }
}
