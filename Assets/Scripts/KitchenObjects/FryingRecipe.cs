using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FryingRecipe", menuName = "Recipes/Create new frying recipe")]
public class FryingRecipe : ScriptableObject
{
    [SerializeField] private KitchenObjectTemplate inputObj;
    [SerializeField] private KitchenObjectTemplate fryedObj;
    [SerializeField] private float totalFryingTime;

    public KitchenObjectTemplate InputObj => inputObj;
    public KitchenObjectTemplate FryedObj => fryedObj;
    public float TotalFryingTime => totalFryingTime;
}
