using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectTemplete kitchenObjectTemplate;

    public override void Interact(Player player)
    {
        GameObject spawnedKitchenObject = Instantiate(kitchenObjectTemplate.Prefab);
        spawnedKitchenObject.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
    }
}
