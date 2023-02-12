using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanCarryKitchenObject
{
    KitchenObject KitchenObj { get; set;}

    Transform GetParentTransform();
    public void ClearKitchenObject();
    public bool HasKitchenObject();

}
