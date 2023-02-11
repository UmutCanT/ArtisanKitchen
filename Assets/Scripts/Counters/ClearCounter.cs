using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectTemplete kitchenObjectTemplate;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.KitchenObject.SetKitchenObjectParent(this);
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {

            }
            else
            {
                KitchenObject.SetKitchenObjectParent(player);
            }
        }
    }
}
