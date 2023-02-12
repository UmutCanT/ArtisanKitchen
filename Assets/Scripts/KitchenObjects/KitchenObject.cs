using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectTemplete objectTemplete;

    private ICanCarryKitchenObject kitchenObjectParent;

    public KitchenObjectTemplete ObjectTemplete => objectTemplete;

    public static KitchenObject SpawnKitchenObject(KitchenObjectTemplete objectTemplete, ICanCarryKitchenObject parent)
    {
        GameObject spawnedKitchenObject = Instantiate(objectTemplete.Prefab);
        KitchenObject kitchenObject = spawnedKitchenObject.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(parent);
        return kitchenObject;
    }

    public void SetKitchenObjectParent(ICanCarryKitchenObject kitchenObjectParent) 
    {
        this.kitchenObjectParent?.ClearKitchenObject();

        this.kitchenObjectParent = kitchenObjectParent;
        kitchenObjectParent.KitchenObj = this;
        transform.parent = kitchenObjectParent.GetParentTransform();
        transform.localPosition = Vector3.zero;
    }

    public void RemovingItself()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }
}
