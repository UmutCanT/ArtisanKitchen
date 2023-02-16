using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DishRecipe", menuName = "Recipes/Create new dish recipe")]
public class DishRecipe : ScriptableObject
{
    [SerializeField] private string dishName;
    [SerializeField] private List<KitchenObjectTemplate> dishIngredients;

    public string DishName => dishName;
    public List<KitchenObjectTemplate> DishIngredients => dishIngredients;
}
