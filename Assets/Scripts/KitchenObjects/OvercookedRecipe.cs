using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OvercookedRecipe", menuName = "Recipes/Create new overcooked recipe")]
public class OvercookedRecipe : ScriptableObject
{
    [SerializeField] private KitchenObjectTemplate inputObj;
    [SerializeField] private KitchenObjectTemplate burnedObj;
    [SerializeField] private float totalOvercookTime;

    public KitchenObjectTemplate InputObj => inputObj;
    public KitchenObjectTemplate BurnedObj => burnedObj;
    public float TotalOvercookTime => totalOvercookTime;
}
