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
            GameObject spawnedKitchenObject = Instantiate(kitchenObjectTemplate.Prefab);
            spawnedKitchenObject.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPlayerOpenContainer?.Invoke(this, EventArgs.Empty);
        }     
    }
}
