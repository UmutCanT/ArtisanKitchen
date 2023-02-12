using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CuttingRecipe", menuName = "Cutting Recipes/Create new recipe")]
public class CuttingRecipe : ScriptableObject
{
    [SerializeField] private KitchenObjectTemplete rawObj;
    [SerializeField] private KitchenObjectTemplete processedObj;

    public KitchenObjectTemplete RawObj => rawObj; 
    public KitchenObjectTemplete ProcessedObj => processedObj;
}
