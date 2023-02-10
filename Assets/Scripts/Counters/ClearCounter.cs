using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, ICanCarryKitchenObject
{
    [SerializeField] private KitchenObjectTemplete objTemp;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;
    public KitchenObject KitchenObject { get => kitchenObject; set => kitchenObject = value; }

    public void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            GameObject spawnedKitchenObject = Instantiate(objTemp.Prefab, counterTopPoint);
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
