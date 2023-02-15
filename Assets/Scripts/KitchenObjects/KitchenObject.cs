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

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }

        plateKitchenObject = null;
        return false;
    }

    public void SetKitchenObjectParent(ICanCarryKitchenObject kitchenObjectParent) 
    {
        this.kitchenObjectParent?.ClearKitchenObject();

        this.kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Counter already has kitchen object");
        }
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
