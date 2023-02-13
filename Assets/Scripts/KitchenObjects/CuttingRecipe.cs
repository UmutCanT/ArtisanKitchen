using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CuttingRecipe", menuName = "Recipes/Create new cutting recipe")]
public class CuttingRecipe : ScriptableObject
{
    [SerializeField] private KitchenObjectTemplate rawObj;
    [SerializeField] private KitchenObjectTemplate processedObj;
    [SerializeField] private int numberOfStepsToProcess;

    public KitchenObjectTemplate RawObj => rawObj; 
    public KitchenObjectTemplate ProcessedObj => processedObj;
    public int NumberOfStepsToProcess => numberOfStepsToProcess;
}
