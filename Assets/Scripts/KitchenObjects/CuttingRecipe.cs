using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CuttingRecipe", menuName = "Cutting Recipes/Create new recipe")]
public class CuttingRecipe : ScriptableObject
{
    [SerializeField] private KitchenObjectTemplate rawObj;
    [SerializeField] private KitchenObjectTemplate processedObj;
    [SerializeField] int numberOfStepsToProcess;

    public KitchenObjectTemplate RawObj => rawObj; 
    public KitchenObjectTemplate ProcessedObj => processedObj;
    public int NumberOfStepsToProcess => numberOfStepsToProcess;
}
