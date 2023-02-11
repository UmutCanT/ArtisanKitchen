using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectTemplete objectTemplete;

    private ICanCarryKitchenObject kitchenObjectParent;

    public KitchenObjectTemplete ObjectTemplete => objectTemplete;

    public void SetKitchenObjectParent(ICanCarryKitchenObject kitchenObjectParent) 
    {
        this.kitchenObjectParent?.ClearKitchenObject();

        this.kitchenObjectParent = kitchenObjectParent;
        kitchenObjectParent.KitchenObject = this;
        transform.parent = kitchenObjectParent.GetParentTransform();
        transform.localPosition = Vector3.zero;
    }
}
