using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerOpenContainer;

    [SerializeField] private KitchenObjectTemplete kitchenObjectTemplate;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectTemplate, player);
            OnPlayerOpenContainer?.Invoke(this, EventArgs.Empty);
        }     
    }
}
