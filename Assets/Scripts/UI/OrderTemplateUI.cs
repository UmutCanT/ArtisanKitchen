using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OrderTemplateUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI orderName;
    [SerializeField] private GameObject ingredientsUI;
    [SerializeField] private GameObject ingredientIconTemplate;

    private void Awake()
    {
        ingredientIconTemplate.SetActive(false);
    }

    public void SetOrderName(DishRecipe dish)
    {
        orderName.text = dish.DishName;
        UpdateIngredientsUI(dish);
    }

    private void UpdateIngredientsUI(DishRecipe dish)
    {
        foreach (KitchenObjectTemplate ingredient in dish.DishIngredients)
        {
            GameObject icon = Instantiate(ingredientIconTemplate, ingredientsUI.transform);
            icon.SetActive(true);
            icon.GetComponent<Image>().sprite = ingredient.Sprite;
        }
    }  
}
