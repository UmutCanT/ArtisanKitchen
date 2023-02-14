using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

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
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
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
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipe.NumberOfStepsToProcess
            });

            if (cuttingProgress >= cuttingRecipe.NumberOfStepsToProcess)
            {
                KitchenObjectTemplate processedObj = GetProcessedKitchenObject(KitchenObj.ObjectTemplate);
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
    
    private KitchenObjectTemplate GetProcessedKitchenObject(KitchenObjectTemplate rawObj)
    {
        CuttingRecipe cuttingRecipe = GetCuttingRecipeWithRawObj(rawObj);
        if (cuttingRecipe != null)
        {
            return cuttingRecipe.ProcessedObj;
        }
        else
        {
            return null;
        }
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
