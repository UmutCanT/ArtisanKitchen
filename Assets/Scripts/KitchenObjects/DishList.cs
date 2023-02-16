using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DishList", menuName = "Dish List/Create new dish list")]
public class DishList : ScriptableObject
{
    [SerializeField] private List<DishRecipe> dishRecipeList;
    public List<DishRecipe> DishRecipeList => dishRecipeList; 
}
