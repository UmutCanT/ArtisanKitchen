using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipe[] fryingRecipes;
    private FryingRecipe fryingRecipe;
    private float fryingTimer;

    private void Update()
    {
        if (HasKitchenObject())
        {
            fryingTimer += Time.deltaTime;
            if (fryingTimer >= fryingRecipe.TotalFryingTime)
            {
                fryingTimer = 0;
                KitchenObj.RemovingItself();
                KitchenObject.SpawnKitchenObject(fryingRecipe.FryedObj, this);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (InputObjectHasRecipe(player.KitchenObj.ObjectTemplate))
                {
                    player.KitchenObj.SetKitchenObjectParent(this); 
                    fryingRecipe = GetFryingRecipeWithInputObj(KitchenObj.ObjectTemplate);
                }
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {

            }
            else
            {
                KitchenObj.SetKitchenObjectParent(player);
            }
        }
    }


    private bool InputObjectHasRecipe(KitchenObjectTemplate inputObj)
    {
        FryingRecipe fryingRecipe = GetFryingRecipeWithInputObj(inputObj);
        return fryingRecipe != null;
    }

    private KitchenObjectTemplate GetFryedKitchenObject(KitchenObjectTemplate inputObj)
    {
        FryingRecipe fryingRecipe = GetFryingRecipeWithInputObj(inputObj);
        if (fryingRecipe != null)
        {
            return fryingRecipe.FryedObj;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipe GetFryingRecipeWithInputObj(KitchenObjectTemplate inputObj)
    {
        foreach (FryingRecipe recipe in fryingRecipes)
        {
            if (recipe.InputObj == inputObj)
            {
                return recipe;
            }
        }
        return null;
    }
}
