using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectTemplate objectTemplate;

    private ICanCarryKitchenObject kitchenObjectParent;

    public KitchenObjectTemplate ObjectTemplate => objectTemplate;

    public static KitchenObject SpawnKitchenObject(KitchenObjectTemplate objectTemplate, ICanCarryKitchenObject parent)
    {
        GameObject spawnedKitchenObject = Instantiate(objectTemplate.Prefab);
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
