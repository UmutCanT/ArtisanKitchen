using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconTemplateUI : MonoBehaviour
{
    [SerializeField] private Image icon;

    public void SetKitchenObjectTemplateIcon(KitchenObjectTemplate kitchenObjectTemplate) 
    {
        icon.sprite = kitchenObjectTemplate.Sprite;
    }
}
