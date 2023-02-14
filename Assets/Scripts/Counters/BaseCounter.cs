using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, ICanCarryKitchenObject
{
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;
    public KitchenObject KitchenObj { get => kitchenObject; set => kitchenObject = value; }

    public virtual void Interact(Player player) 
    {
        Debug.LogError("BaseCounter.Interact();");
    }
    public virtual void InteractAlternate(Player player) 
    {
    }

    public Transform GetParentTransform() => counterTopPoint;

    public void ClearKitchenObject() => kitchenObject = null;

    public bool HasKitchenObject() => kitchenObject != null;
}
