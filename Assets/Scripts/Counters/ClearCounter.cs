using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.KitchenObj.SetKitchenObjectParent(this);
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
}
