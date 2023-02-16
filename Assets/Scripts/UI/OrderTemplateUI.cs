using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderTemplateUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI orderName;

    public void SetOrderName(DishRecipe dish)
    {
        orderName.text = dish.DishName;
    }
}
