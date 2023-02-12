using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] CuttingRecipe[] cuttingRecipes;

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
            KitchenObjectTemplate processedObj = GetProcessedKitchenObject(KitchenObj.ObjectTemplate);
            KitchenObj.RemovingItself();
            KitchenObject.SpawnKitchenObject(processedObj, this);
        }
    }

    private bool RawObjectHasRecipe(KitchenObjectTemplate rawObj)
    {
        foreach (CuttingRecipe recipe in cuttingRecipes)
        {
            if (recipe.RawObj == rawObj)
            {
                return true;
            }
        }
        return false;
    }
    
    private KitchenObjectTemplate GetProcessedKitchenObject(KitchenObjectTemplate rawObj)
    {
        foreach (CuttingRecipe recipe in cuttingRecipes)
        {
            if (recipe.RawObj == rawObj)
            {
                return recipe.ProcessedObj;
            }
        }
        return null;
    }
}
