using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, ICanProgress
{
    public event EventHandler<ICanProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipe[] cuttingRecipes;
    private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (RawObjectHasRecipe(player.KitchenObj.ObjectTemplate))
                {
                    player.KitchenObj.SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingRecipe cuttingRecipe = GetCuttingRecipeWithRawObj(KitchenObj.ObjectTemplate);
                    OnProgressChanged?.Invoke(this, new ICanProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipe.NumberOfStepsToProcess
                    });
                }             
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (player.KitchenObj.TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(KitchenObj.ObjectTemplate))
                    {
                        KitchenObj.RemovingItself();
                    }
                }
            }
            else
            {
                KitchenObj.SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && RawObjectHasRecipe(KitchenObj.ObjectTemplate))
        {
            cuttingProgress++;
            CuttingRecipe cuttingRecipe = GetCuttingRecipeWithRawObj(KitchenObj.ObjectTemplate);

            OnCut?.Invoke(this, EventArgs.Empty);
            OnProgressChanged?.Invoke(this, new ICanProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipe.NumberOfStepsToProcess
            });

            if (cuttingProgress >= cuttingRecipe.NumberOfStepsToProcess)
            {
                KitchenObjectTemplate processedObj = GetCuttingRecipeWithRawObj(KitchenObj.ObjectTemplate).ProcessedObj;
                KitchenObj.RemovingItself();
                KitchenObject.SpawnKitchenObject(processedObj, this);
            }       
        }
    }

    private bool RawObjectHasRecipe(KitchenObjectTemplate rawObj)
    {
        CuttingRecipe cuttingRecipe = GetCuttingRecipeWithRawObj(rawObj);
        return cuttingRecipe != null;       
    }
   
    private CuttingRecipe GetCuttingRecipeWithRawObj(KitchenObjectTemplate rawObj) 
    {
        foreach (CuttingRecipe recipe in cuttingRecipes)
        {
            if (recipe.RawObj == rawObj)
            {
                return recipe;
            }
        }
        return null;
    }
}
