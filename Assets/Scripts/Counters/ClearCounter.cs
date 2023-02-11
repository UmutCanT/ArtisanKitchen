using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, ICanCarryKitchenObject
{
    [SerializeField] private KitchenObjectTemplete kitchenObjectTemplate;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;
    public KitchenObject KitchenObject { get => kitchenObject; set => kitchenObject = value; }

    public override void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            GameObject spawnedKitchenObject = Instantiate(kitchenObjectTemplate.Prefab, counterTopPoint);
            spawnedKitchenObject.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            kitchenObject.SetKitchenObjectParent(player);
        }       
    }

    public Transform GetParentTransform() => counterTopPoint;

    public void ClearKitchenObject() => kitchenObject = null;

    public bool HasKitchenObject() => kitchenObject != null;
}
