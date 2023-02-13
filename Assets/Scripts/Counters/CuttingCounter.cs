using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

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
                //if (RawObjectHasRecipe(player.KitchenObj.ObjectTemplate))
                //{
                //    Add this part if u want to disable putting not cuttable objects
                //}
                player.KitchenObj.SetKitchenObjectParent(this);
                cuttingProgress= 0;
                CuttingRecipe cuttingRecipe = GetRecipeWithRawObj(KitchenObj.ObjectTemplate);
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                {
                    progressNormalized = (float)cuttingProgress / cuttingRecipe.NumberOfStepsToProcess
                });
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
            CuttingRecipe cuttingRecipe = GetRecipeWithRawObj(KitchenObj.ObjectTemplate);
            cuttingProgress++;

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
        CuttingRecipe cuttingRecipe = GetRecipeWithRawObj(rawObj);
        return cuttingRecipe != null;       
    }
    
    private KitchenObjectTemplate GetProcessedKitchenObject(KitchenObjectTemplate rawObj)
    {
        CuttingRecipe cuttingRecipe = GetRecipeWithRawObj(rawObj);
        if (cuttingRecipe != null)
        {
            return cuttingRecipe.ProcessedObj;
        }
        else
        {
            return null;
        }
    }

    private CuttingRecipe GetRecipeWithRawObj(KitchenObjectTemplate rawObj) 
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
