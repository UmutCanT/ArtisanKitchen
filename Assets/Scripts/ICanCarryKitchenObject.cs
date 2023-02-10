using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanCarryKitchenObject
{
    KitchenObject KitchenObject { get; set;}

    Transform GetParentTransform();
    public void ClearKitchenObject();
    public bool HasKitchenObject();

}
